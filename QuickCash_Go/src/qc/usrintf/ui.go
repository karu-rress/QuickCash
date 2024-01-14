package usrintf

import (
	"bufio"
	"fmt"
	"ogl"
	"os"
	"qc/cshdat"
	"sort"
)

// UI structure
type UI struct {
	chores cshdat.ChoresMap
	data   cshdat.CashList
	shots  uint16
	strbuf string
}

// MakeUI creates UI sturcture
func MakeUI() *UI {
	return &UI{chores: make(cshdat.ChoresMap), data: make([]cshdat.CashData, 0, 10)}
}

// Read reads saved data
func (ui UI) Read() {
	file, err := os.Open("/home/sunwoo/qc/data.qc")
	if err != nil {
		return
	}
	defer file.Close()

	var s string
	var d, t uint32
	var in *bufio.Reader

	fmt.Fscanf(file, "%d %d", &d, &t)

	for {
		in = bufio.NewReader(file)
		s, err = in.ReadString('\n')
		if err != nil {
			break
		}
		fmt.Sscanf(s, "%d %d", &d, &t)
		ui.data.PushBack(cshdat.NewCashDataWithClock(d, t, s[15:]))
	}
}

// PrintMenu prints menu on screen
func (ui UI) PrintMenu() {
	ogl.Clear()

	fmt.Print("Index\tDate\t\tTime\t\tWork\n\n")
	sort.Sort(ui.data)

	for i, data := range ui.data {
		switch {
		case ui.shots == 1 && (i >= 0 && i < 20):
			fmt.Printf("%d\t%d\t%d\t\t%s\n", i, data.Date(), data.Time(), data.Chore())
		case ui.shots == 2 && (i >= 20 && i < 40):
			fmt.Printf("%d\t%d\t%d\t\t%s\n", i, data.Date(), data.Time(), data.Chore())
		default:
			if uint16(i) >= uint16(i-1)*ui.shots && uint16(i) < (ui.shots*uint16(i)) {

			}
		}
		// if i > shots-1 * 20 && i <= shots * 20
		if (uint32(i-1) >= (ui.shots+(ui.shots*19))-20) && (uint32(i-1) < ui.shots*19) {
			fmt.Printf("%d\t%d\t\t%s\n", data.Date(), data.Time(), data.Chore())
		}
	}
	ogl.Gotoxy(0, 23)
	fmt.Println("[[A]dd\t[R]emove\t[N]ext\t[P]rev\t[Q]uit")
	fmt.Print(">>")
}

func upper(ch byte) byte {
	if ch >= 'a' && ch <= 'z' {
		return ch - 32
	}
	return ch
}

// Run handles program
func (ui UI) Run() {
	ui.Read()
	// defer ui.Save()
	for {
		ui.PrintMenu()
		var ch byte
		switch fmt.Scan(&ch); upper(ch) {
		case 'A':
			ui.Add()
		case 'D': // ui.Remove()
		case 'N':
		case 'P':
		case 'Q':

		}
	}
}

// Add adds cash memo
func (ui *UI) Add() {
	ogl.Clear()
	fmt.Println("What chore did you do?")
	fmt.Print(">>> ")

	in := bufio.NewReader(os.Stdin)
	s, _ := in.ReadString('\n')

	// If it exists...
	if value, err := ui.chores.Find(s); err != nil {
		for {
			var ch byte
			fmt.Printf("%s: is %d, right? [y, n]: ", s, value)
			if fmt.Scan(&ch); ch == 'y' || ch == 'Y' {
				break
			} else if ch == 'n' || ch == 'N' {
				var money int32
				fmt.Print("Then input a new value: ")
				fmt.Scan(&money)
				ui.chores.Assign(s, money)
				fmt.Printf("%s is now %d. Press any key to continue...", s, money)
				fmt.Scanln()
				break
			} else {
				fmt.Println("Please input correctly.")
				continue
			}
		}
	} else { // if it doesn't exist
		fmt.Print(s, "doesn't exist. Please input a new value: ")
		var n int32
		fmt.Scan(&n)
		ui.chores.Assign(s, n)
		fmt.Printf("%s is now %d. Press any key to continue...", s, n)
		fmt.Scanln()
	}

	ui.data.PushBack(cshdat.NewCashData(s))
}

// Remove removes element of cshdat.CashList
func (ui *UI) Remove() {
	ogl.Clear()

	fmt.Print("Input index: ")
	var n uint32
	fmt.Scan(&n)
	ui.data.Remove(n)
}

func (ui UI) Save() {
	file, _ := os.Create("/home/sunwoo/.qc/data.qc")
	defer file.Close()
	for _, value := range ui.data {
		fmt.Fprintln(file, value.Date(), value.Time(), value.Chore())
	}
}
