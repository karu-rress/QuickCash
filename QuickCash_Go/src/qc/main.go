package main

import (
	"fmt"
	// _ "ogl"
)

/*
cout << "\033[1;31m"
         foreground background
black        30         40
red          31         41
green        32         42
yellow       33         43
blue         34         44
magenta      35         45
cyan         36         46
white        37         47

reset             0  (everything back to normal)
bold/bright       1  (often a brighter shade of the same colour)
underline         4
inverse           7  (swap foreground and background colours)
bold/bright off  21
underline off    24
inverse off      27

void textcolor(int foreground, int background)
{
int color=foreground+background*16;
SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);
}

*/

const version = "0.1"
const is64bit = (32<<uintptr(^uintptr(0)>>63) == 64)

func main() {
	_ = version
	_ = is64bit

	fmt.Printf("\033[%d;%df", 20, 15)
	fmt.Println("Oh yeah")
}

/*
func TimeToUInts(tm time.Time) (date uint32, time uint32, err error) {
	var y string
	y = strings.TrimLeft(y, "20")

	fmt.Println(y)
	return
}
*/
