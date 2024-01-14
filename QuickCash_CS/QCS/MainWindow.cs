//
// MainWindow.cs
//

using System;
using Gtk;
using RC;
using Osl;
using System.Threading.Tasks;
using DAT = RC.RollingData;

enum ComboBox
{
    Normal,
    InputSelf,
    Memo
}

public partial class MainWindow : Window, IDisposable
{
    public static Window ThisWindow { get; set; }
    public static Label RcLabel { get; set; }
    public static bool TextChanged { get; private set; } = DAT.IsBeta;
    public static bool PriceChanged { get; private set; } = TextChanged;
    int undoable = 0;
    bool isListViewMode = false;

    bool PriceVisible
    {
        get => price_entry.Visible && pricebox_label.Visible;
        set => price_entry.Visible = pricebox_label.Visible = value;
    }

    static partial void ShowMsgBox(string msg, MessageType mt, ButtonsType button, ref ResponseType response);
    public static void ShowMessage(string msg, MessageType mt, ButtonsType btn, out ResponseType response)
    {
        response = default;
        ShowMsgBox(msg, mt, btn, ref response);
    }

    partial void UpdateTotal();
    partial void UpdateComboBox();
    partial void Prepare();
    public MainWindow() : base(WindowType.Toplevel)
    {
#pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor
        Build();
#pragma warning restore RECS0021
        Prepare();
    }

    partial void DaySelected(Calendar calendar);
    protected void DaySelectedEvent(object sender, EventArgs e)
    {
        if (sender is Calendar date)
            DaySelected(date);
    }

    protected async void ButtonClickedEvent(object sender, EventArgs e)
    {
        await RCFeatures.DoAsyncSave();
        TextChanged = false;
    }

    protected void MemoEditingDoneEvent(object sender, EventArgs e) => add_button.Click();
    partial void ChangeView(ComboBox state);
    protected void ComboBoxChangedEvent(object sender, EventArgs e)
    {
        switch ((sender as ComboBoxEntry).ActiveText)
        {
            case DAT.NormalMemo:
                ChangeView(ComboBox.Memo);
                return;
            case DAT.InputSelf:
                ChangeView(ComboBox.InputSelf);
                return;
            default:
                ChangeView(ComboBox.Normal);
                return;
        }
    }

    partial void AddButtonClicked();
    protected void AddButtonClickedEvent(object sender, EventArgs e) => AddButtonClicked();
    protected void ChangedEvent(object sender, EventArgs e) => PriceChanged = true;
    override public void Dispose() => RCFeatures.Save();

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    //protected void MemoEntryKeyPressedEvent(object sender, KeyPressEventArgs a)
    //{
    //    rc_label.Text = "ddd";
    //   Gdk.EventKey k = a.Event;

    //    if (a.Event.Key is Gdk.Key.KP_Enter || a.Event.Key is Gdk.Key.ISO_Enter || a.Event.Key is Gdk.Key.Key_3270_Enter || a.Event.Key == Gdk.Key.Return)
    //   {
    ///       AddButton.Click();
    //     }
    //}

    protected void ListViewButtonClickedEvent(object sender, EventArgs e)
    {
        if (!isListViewMode)
        {
            isListViewMode = true;
            memo_txtview.Buffer.Text = string.Empty;
            System.Text.StringBuilder sb = new(100);
            foreach (var item in DAT.Memo)
            {
                sb.Append($"{item.Key}:\n");
                foreach (var tuple in item.Value)
                {
                    sb.Append((tuple.price is null) ? $"\t{tuple.chore}\n" : $"\t{tuple.chore}: {tuple.price}\n");
                }
                sb.Append('\n');
            }
            memo_txtview.Println(fontsize: 18, msg: sb.ToString());
            return;
        }
        isListViewMode = false;
        calendar3.SelectDay(uint.Parse($"{date_label.Text[8]}{date_label.Text[9]}"));
    }

    protected void MemoEntryKeyReleaseEvent(object o, KeyReleaseEventArgs args)
    {
        if (args.Event.Key is Gdk.Key.Return)
        {
            add_button.Click();
        }
    }

    protected void UndoButtonClickedEvent(object sender, EventArgs e)
    {
        var target = DAT.MemoUndo[undoable - 1];
        if (DAT.Memo.Exists(target.date, target.chore, target.price))
        {
            foreach (var item in DAT.Memo[target.date])
            {
                if (item.chore == target.chore && item.price == target.price)
                {
                    DAT.Memo[target.date].Remove(item);
                    DAT.MemoUndo.Remove(target);
                    undo_button.Sensitive = !(--undoable is 0);
                    calendar3.SelectDay(uint.Parse($"{date_label.Text[8]}{date_label.Text[9]}"));
                    UpdateTotal();
                    return;
                }
            }
        }
        else
        {
            if (DAT.Memo.ContainsKey(target.date))
                DAT.Memo[target.date].Add((target.chore, target.price));
            else
                DAT.Memo[target.date] = new System.Collections.Generic.List<(string, int?)> { (target.chore, target.price) };
        }
    }
}
