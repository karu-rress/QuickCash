package cshdat

import (
	"reflect"
	"testing"
)

/*
type CashData struct { date, time uint32; chore string }

type CashList []CashData

func (cash CashList) Len() int
func (cash CashList) Less(i, j int) bool
func (cash CashList) Swap(i, j int)

type ChoresMap map[string]int32

func (cm ChoresMap) Find(s string) (int32, error) {
	if value, ok := cm[s]; ok {
		return value, nil
	}
	return 0, fmt.Errorf("%s doesn't exist", s)
}

func (cm *ChoresMap) Assign(s string, n int32) {
	(*cm)[s] = n
}
*/
func TestAllocCshDat(t *testing.T) {
	if b := reflect.TypeOf(*NewCashData("")).Name() == "CashData"; b != true {
		t.Error("NewCashData의 반환값이 CashData가 아닙니다. 타입: ", reflect.TypeOf(*NewCashData("")).Name())
	}
	if b := reflect.TypeOf(*NewCashDataWithClock(0, 0, "")).Name() == "CashData"; b != true {
		t.Error("NewCashData의 반환값이 CashData가 아닙니다. 타입: ", reflect.TypeOf(*NewCashDataWithClock(0, 0, "")).Name())
	}
}

func TestCpy(t *testing.T) {
	var cd = CashData{1, 2, "3"}
	var ce CashData
	if ce.Copy(cd); !(ce.date == cd.date && ce.time == cd.time && ce.chore == cd.chore) {
		t.Errorf("Copy의 복사가 원활하지 않습니다. cd = {%d, %d, %s}, ce = {%d, %d, %s}", cd.date, cd.time, cd.chore, ce.date, ce.time, ce.chore)
	}
}

func TestProperty(t *testing.T) {
	var c = CashData{1000, 2000, "3000"}
	if c.Date() != 1000 || c.Time() != 2000 || c.Chore() != "3000" {
		t.Errorf("접근자 메서드의 반환이 올바르지 않습니다. c = {%d, %d, %s}, Date(), Time()m Chore() = {%d, %d, %s}", c.date, c.time, c.chore, c.Date(), c.Time(), c.Chore())
	}
}

func TestPushAndPop(t *testing.T) {
	c := make(CashList, 0, 10)
	for i := 0; i < 5; i++ {
		c.PushBack(&CashData{uint32(i), 0, ""})
	}

	if l := len(c); l != 5 {
		t.Errorf("PushBack이 요소를 추가하지 못했습니다. len(c) = %d, cap(c) = %d", l, cap(c))
	}

	for i, value := range c {
		if value.Date() != uint32(i) {
			t.Error("요소의 순서가 올바르지 않습니다.")
		}
	}

	for i := 0; i < 3; i++ {
		c.PopBack()
	}

	if l := len(c); l != 2 {
		t.Errorf("PopBack이 요소를 제거하지 못했습니다. len(c) = %d, cap(c) = %d", l, cap(c))
	}

	for i, value := range c {
		if value.Date() != uint32(i) {
			t.Error("요소의 순서가 올바르지 않습니다.")
		}
	}

	if c.ClearElem(); c.Len() != 0 {
		t.Error("Clear가 모든 원소를 제거하지 못했습니다. len(c) =", len(c))
	}

	for i := 4; i >= 0; i-- {
		c.PushFront(&CashData{uint32(i), 0, ""})
	}

	if len(c) != 5 {
		t.Errorf("PushFront가 요소를 추가하지 못했습니다. len(c) = %d, cap(c) = %d", len(c), cap(c))
	}

	for i, value := range c {
		if value.Date() != uint32(i) {
			t.Error("요소의 순서가 올바르지 않습니다.")
		}
	}

	for i := 0; i < 3; i++ {
		c.PopFront()
	}

	if len(c) != 5-3 {
		t.Errorf("PopFront가 요소를 제거하지 못했습니다. len(c) = %d, cap(c) = %d", len(c), cap(c))
	}

	for i, value := range c {
		if value.Date() != uint32(i+3) {
			t.Error("요소의 순서가 올바르지 않습니다.")
		}
	}

	if c.Clear(); len(c) != 0 || cap(c) != 0 {
		t.Error("Clear가 초기화에 실패하였습니다. len(c) =", len(c), "cap(c) =", cap(c))
	}
}

func TestRemove(t *testing.T) {
	c := CashList{
		CashData{0, 0, ""},
		CashData{1, 0, ""},
		CashData{2, 0, ""},
		CashData{3, 0, ""},
		CashData{4, 0, ""},
	}

	c.Remove(2)
	if c.ExistWithDate(2) == true {
		t.Error("Remove가 삭제에 실패하였습니다.")
	}

	for _, value := range c {
		if value.Date() == 2 {
			t.Error("Remove가 삭제에 실패하였습니다.")
		}
	}
}
