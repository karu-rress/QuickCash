//
// PartialMainWindow.cs
//
using System;
using System.Collections.Generic;
using System.Text;
using Gtk;
using Osl;
using RC;
using DAT = RC.RollingData;

public partial class MainWindow : Gtk.Window, IDisposable
{
    partial void DaySelected(Gtk.Calendar calendar)
    {
        memo_txtview.Buffer.Clear();
        calendar.PrintToLabel(ref date_label, ref day_label);
        DAT.Memo.TryGetValue($"{calendar.Year}-{calendar.Month + 1:00}-{calendar.Day:00}", out var dataList);

        if (dataList is null)
        {
            memo_txtview.Println(msg: "(Nothing)");
            return;
        }

        StringBuilder sb = new StringBuilder(20), sb2 = new StringBuilder(20);
        foreach (var item in dataList)
        {
            if (item.price is null)
                sb2.Append($"{item.chore}\n");
          else 
                sb.Append($"{item.chore}: {item.price.Value}\n");
        }
        if (sb.ToString().IsNotNullOrWhiteSpace() && sb2.ToString().IsNotNullOrWhiteSpace())
        {
            memo_txtview.Println(msg: sb2.ToString().TrimEnd('\n')); 
            memo_txtview.Println(msg: "==================================");
            memo_txtview.Println(msg: sb.ToString().TrimEnd('\n'));
            return;
        }

        if (string.IsNullOrWhiteSpace(sb2.ToString()))
            memo_txtview.Println(msg: sb.ToString().TrimEnd('\n'));
        else
            memo_txtview.Println(msg: sb2.ToString().TrimEnd('\n'));
    }

    partial void UpdateTotal()
    {
        int total = default;
        try
        {
            foreach (List<(string, int?)> items in DAT.Memo.Values)
            {
                foreach ((string, int?) item2 in items)
                {
                    checked
                    {
                        total += item2.Item2 ?? 0;
                    }
                }
            }
        }
        catch(OverflowException e)
        {
            ShowMessage(e.ToString(), MessageType.Error, ButtonsType.Ok, out var _);   
        }
        finally
        {
            total_label.Print(msg: $"{DAT.Won}{total}");
        }
    }

    partial void UpdateComboBox()
    {
        (chore_box.Model as ListStore).Clear(); 
        foreach (var item in DAT.ChoreMap.Keys)
        {
            chore_box.AppendText(item);
        }
        chore_box.AppendText(DAT.InputSelf);
        chore_box.AppendText(DAT.NormalMemo);
    }

    partial void Prepare()
    {
        RCFeatures.DoAsyncRead().GetAwaiter().GetResult();

        RcLabel = rc_label;
        rc_label.Print(38, DAT.ProgramName);
        version_label.Print(28, $"v{DAT.Version:0.00} {DAT.BETA}");
        rc_sublabel.Print(19, DAT.Description);
        date_label.Print(msg: $"{DAT.Calendar.Year}-{DAT.Calendar.Month + 1:00}-{DAT.Calendar.Day:00}");
        day_label.Print(msg: DAT.Calendar.ToDayString());
        total_txt_label.Print(msg: DAT.Total);
        memo_txtview.Println(msg: DAT.SelectDay);
        add_button.Label = DAT.Add;

        UpdateTotal();
        UpdateComboBox();
        //rc_label.Text = DAT.Memo?.Count.ToString() ?? "Error";
    }

    static partial void ShowMsgBox(string msg, MessageType mt, ButtonsType button, ref ResponseType response)
    {
        MessageDialog dialog = null;
        try
        {
            dialog = new MessageDialog(ThisWindow, DialogFlags.DestroyWithParent | DialogFlags.Modal, mt, button, msg);
            response = (ResponseType)dialog.Run();
        }
        finally
        {
            dialog?.Destroy();
        }
    }

    partial void AddButtonClicked()
    {
        undo_button.Sensitive = !(++undoable is 0); 
        if (memo_entry.Text.Contains("|") || chore_box.ActiveText.Contains("|"))
        {
            //ShowMessage()
            //return;
        }
        switch (chore_box.ActiveText)
        {
            case DAT.NormalMemo:
                if (DAT.Memo.ContainsKey(date_label.Text))
                    DAT.Memo[date_label.Text].Add((memo_entry.Text, null));
                else
                    DAT.Memo[date_label.Text] = new List<(string chore, int? price)> { (memo_entry.Text, null) };

                DAT.MemoUndo.Add((date_label.Text, memo_entry.Text, null));
                break;

            default:
                if (string.IsNullOrEmpty(price_entry.Text))
                {
                    ShowMessage(DAT.InputPrice, MessageType.Warning, ButtonsType.Ok, out var _);
                    return;
                }
                bool parsed = int.TryParse(price_entry.Text, out int price);
                if (!parsed && price_entry.Text.Length >= 10)
                {
                    ShowMessage(DAT.TooBigPrice, MessageType.Error, ButtonsType.Ok, out var _);
                    return;
                }
                if (parsed)
                {
                    if (DAT.Memo.ContainsKey(date_label.Text))
                        DAT.Memo[date_label.Text].Add((chore_box.ActiveText, price));
                    else
                        DAT.Memo[date_label.Text] = new List<(string chore, int? price)> { (chore_box.ActiveText, price) };
                    
                    DAT.MemoUndo.Add((date_label.Text, chore_box.ActiveText, price));
                    if (PriceChanged)
                    {
                        ShowMessage(DAT.TextChangedMessage, MessageType.Question, ButtonsType.YesNo, out var response);
                        if (response is ResponseType.Yes)
                            DAT.ChoreMap[chore_box.ActiveText] = price;
                    }
                }
                else
                    ShowMessage(DAT.PriceInputError, MessageType.Error, ButtonsType.Ok, out var _);
                break;
        }
        calendar3.SelectDay(uint.Parse($"{date_label.Text[8]}{date_label.Text[9]}"));
        UpdateTotal();
        UpdateComboBox();
        memo_entry.Text = string.Empty;
    }

    partial void ChangeView(ComboBox state)
    {
        switch (state)
        {
            case ComboBox.Normal:
                pricebox_label.Visible = true;
                memo_entry.Text = string.Empty;

                memo_entry.Visible = false;
                inputbox_label.Visible = false;
                memobox_label.Visible = false;
                if (DAT.ChoreMap.TryGetValue(chore_box.ActiveText, out int? value))
                {
                    price_entry.Text = value.ToString();
                    PriceChanged = false;
                }
                return;

            case ComboBox.InputSelf:
                chore_box.Entry.Text = string.Empty;
                price_entry.Text = string.Empty;
                memo_entry.Text = string.Empty;

                memo_entry.Visible = false;
                memobox_label.Visible = false;

                pricebox_label.Visible = true;
                inputbox_label.Visible = true;
                return;

            case ComboBox.Memo:
                price_entry.Text = string.Empty;
                inputbox_label.Visible = false;
                pricebox_label.Visible = false;

                memo_entry.Visible = true;
                memobox_label.Visible = true;
                return;
        }
    }
}
