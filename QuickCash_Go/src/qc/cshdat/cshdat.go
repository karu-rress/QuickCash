// Package cshdat includes CashData, CashList and ChoresMap
package cshdat

import (
	"fmt"
	conv "ogl"
	"time"
)

// CashData includes date, time uint32 and chore string
type CashData struct {
	date, time uint32
	chore      string
}

// NewCashData constructs new CashData instance
func NewCashData(s string) *CashData {
	var c CashData
	c.date, c.time = conv.TimeToUInt32s(time.Now())
	c.chore = s
	return &c
}

// NewCashDataWithClock creates new CashData instance with date, time
func NewCashDataWithClock(date uint32, tm uint32, s string) *CashData {
	return &CashData{date, tm, s}
}

// Copy copies CashData
func (c *CashData) Copy(c2 CashData) {
	c.date, c.time, c.chore = c2.date, c2.time, c2.chore
}

// Date returns date to uint32
func (c CashData) Date() uint32 {
	return c.date
}

// Time returns time to uint32
func (c CashData) Time() uint32 {
	return c.time
}

// Chore returns chore to string
func (c CashData) Chore() string {
	return c.chore
}

// CashList is slice of CashData. It has PushBack and PopBack method.
type CashList []CashData

// PushBack pushes CashData pointer to back of CashList
func (cash *CashList) PushBack(c *CashData) {
	*cash = append(*cash, *c)
}

// PushFront pushes CashData pointer to front of CashList
func (cash *CashList) PushFront(c *CashData) {
	*cash = append(CashList{*c}, *cash...)
}

// PopBack pops the back element of CashList
func (cash *CashList) PopBack() {
	*cash = (*cash)[:len(*cash)-1]
}

// PopFront pops the front element of CashList
func (cash *CashList) PopFront() {
	*cash = (*cash)[1:]
}

// Remove removes element with index
func (cash *CashList) Remove(index uint32) {
	*cash = append((*cash)[:index], (*cash)[index+1:]...)
}

// ExistWithDate returns bool value:
// if an element with the date of CashList exists, it will be true.
func (cash CashList) ExistWithDate(date uint32) bool {
	for _, value := range cash {
		if value.date == date {
			return true
		}
	}
	return false
}

// ClearElem clears all element of CashList,
// buf does't clear the capacity.
func (cash *CashList) ClearElem() {
	*cash = (*cash)[:0:cap(*cash)]
}

// Clear assigns nil to CashList.
// That is, clears all element and capacity of CashList.
func (cash *CashList) Clear() {
	*cash = nil
}

// Len implements Len of sort.Interface (func Len() int)
func (cash CashList) Len() int {
	return len(cash)
}

// Less implements Less of sort.Interface
func (cash CashList) Less(i, j int) bool {
	if cash[i].date == cash[j].date {
		return cash[i].time < cash[j].time
	}
	return cash[i].date < cash[j].date
}

func (cash CashList) Swap(i, j int) {
	cash[i], cash[j] = cash[j], cash[i]
}

// ChoresMap is map of chores
type ChoresMap map[string]int32

// Find finds the string and return nil if it exist
func (cm ChoresMap) Find(s string) (int32, error) {
	if value, ok := cm[s]; ok {
		return value, nil
	}
	return 0, fmt.Errorf("%s doesn't exist", s)
}

// Assign changed the value of key
func (cm *ChoresMap) Assign(s string, n int32) {
	(*cm)[s] = n
}
