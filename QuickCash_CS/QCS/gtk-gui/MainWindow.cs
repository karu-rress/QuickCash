
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed1;

	private global::Gtk.Calendar calendar3;

	private global::Gtk.Label version_label;

	private global::Gtk.Label rc_sublabel;

	private global::Gtk.Label rc_label;

	private global::Gtk.Label date_label;

	private global::Gtk.Label day_label;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView memo_txtview;

	private global::Gtk.Button add_button;

	private global::Gtk.ComboBoxEntry chore_box;

	private global::Gtk.Entry price_entry;

	private global::Gtk.Label pricebox_label;

	private global::Gtk.Entry memo_entry;

	private global::Gtk.Label memobox_label;

	private global::Gtk.Label inputbox_label;

	private global::Gtk.Label total_txt_label;

	private global::Gtk.Label total_label;

	private global::Gtk.Button undo_button;

	private global::Gtk.Button apply_button;

	private global::Gtk.Button button1;

	private global::Gtk.Entry date_entry;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = "";
		this.Icon = global::Stetic.IconLoader.LoadIcon(this, "gtk-edit", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(1));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed1 = new global::Gtk.Fixed();
		this.fixed1.WidthRequest = 500;
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.calendar3 = new global::Gtk.Calendar();
		this.calendar3.WidthRequest = 399;
		this.calendar3.HeightRequest = 206;
		this.calendar3.CanFocus = true;
		this.calendar3.Name = "calendar3";
		this.calendar3.DisplayOptions = ((global::Gtk.CalendarDisplayOptions)(35));
		this.fixed1.Add(this.calendar3);
		global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.calendar3]));
		w1.X = 841;
		w1.Y = 18;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.version_label = new global::Gtk.Label();
		this.version_label.WidthRequest = 217;
		this.version_label.HeightRequest = 62;
		this.version_label.Name = "version_label";
		this.version_label.LabelProp = global::Mono.Unix.Catalog.GetString("------");
		this.fixed1.Add(this.version_label);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.version_label]));
		w2.X = 497;
		w2.Y = 49;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.rc_sublabel = new global::Gtk.Label();
		this.rc_sublabel.WidthRequest = 504;
		this.rc_sublabel.HeightRequest = 46;
		this.rc_sublabel.Name = "rc_sublabel";
		this.rc_sublabel.LabelProp = global::Mono.Unix.Catalog.GetString("---------------------------------------------------------------------------------" +
				"--");
		this.fixed1.Add(this.rc_sublabel);
		global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.rc_sublabel]));
		w3.X = 229;
		w3.Y = 103;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.rc_label = new global::Gtk.Label();
		this.rc_label.WidthRequest = 404;
		this.rc_label.HeightRequest = 60;
		this.rc_label.Name = "rc_label";
		this.rc_label.LabelProp = "------------------------------------------------------------------";
		this.rc_label.UseMarkup = true;
		this.fixed1.Add(this.rc_label);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.rc_label]));
		w4.X = 118;
		w4.Y = 46;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.date_label = new global::Gtk.Label();
		this.date_label.Name = "date_label";
		this.date_label.LabelProp = global::Mono.Unix.Catalog.GetString("----");
		this.fixed1.Add(this.date_label);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.date_label]));
		w5.X = 114;
		w5.Y = 192;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.day_label = new global::Gtk.Label();
		this.day_label.Name = "day_label";
		this.day_label.LabelProp = global::Mono.Unix.Catalog.GetString("-----");
		this.fixed1.Add(this.day_label);
		global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.day_label]));
		w6.X = 401;
		w6.Y = 194;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow.WidthRequest = 800;
		this.GtkScrolledWindow.HeightRequest = 441;
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.memo_txtview = new global::Gtk.TextView();
		this.memo_txtview.CanFocus = true;
		this.memo_txtview.Name = "memo_txtview";
		this.memo_txtview.Editable = false;
		this.memo_txtview.CursorVisible = false;
		this.memo_txtview.AcceptsTab = false;
		this.GtkScrolledWindow.Add(this.memo_txtview);
		this.fixed1.Add(this.GtkScrolledWindow);
		global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.GtkScrolledWindow]));
		w8.X = 440;
		w8.Y = 253;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.add_button = new global::Gtk.Button();
		this.add_button.WidthRequest = 158;
		this.add_button.CanFocus = true;
		this.add_button.Name = "add_button";
		this.add_button.UseUnderline = true;
		this.fixed1.Add(this.add_button);
		global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.add_button]));
		w9.X = 255;
		w9.Y = 249;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.chore_box = global::Gtk.ComboBoxEntry.NewText();
		this.chore_box.Name = "chore_box";
		this.fixed1.Add(this.chore_box);
		global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.chore_box]));
		w10.X = 17;
		w10.Y = 249;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.price_entry = new global::Gtk.Entry();
		this.price_entry.WidthRequest = 192;
		this.price_entry.HeightRequest = 35;
		this.price_entry.CanFocus = true;
		this.price_entry.Name = "price_entry";
		this.price_entry.IsEditable = true;
		this.price_entry.InvisibleChar = '●';
		this.fixed1.Add(this.price_entry);
		global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.price_entry]));
		w11.X = 18;
		w11.Y = 323;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.pricebox_label = new global::Gtk.Label();
		this.pricebox_label.Name = "pricebox_label";
		this.pricebox_label.LabelProp = global::Mono.Unix.Catalog.GetString("(금액 입력)");
		this.fixed1.Add(this.pricebox_label);
		global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.pricebox_label]));
		w12.X = 20;
		w12.Y = 363;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.memo_entry = new global::Gtk.Entry();
		this.memo_entry.WidthRequest = 394;
		this.memo_entry.HeightRequest = 37;
		this.memo_entry.CanFocus = true;
		this.memo_entry.Name = "memo_entry";
		this.memo_entry.IsEditable = true;
		this.memo_entry.InvisibleChar = '●';
		this.fixed1.Add(this.memo_entry);
		global::Gtk.Fixed.FixedChild w13 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.memo_entry]));
		w13.X = 19;
		w13.Y = 321;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.memobox_label = new global::Gtk.Label();
		this.memobox_label.Name = "memobox_label";
		this.memobox_label.LabelProp = global::Mono.Unix.Catalog.GetString("(메모 입력)");
		this.fixed1.Add(this.memobox_label);
		global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.memobox_label]));
		w14.X = 177;
		w14.Y = 365;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.inputbox_label = new global::Gtk.Label();
		this.inputbox_label.Name = "inputbox_label";
		this.inputbox_label.LabelProp = global::Mono.Unix.Catalog.GetString("(직접 입력)");
		this.fixed1.Add(this.inputbox_label);
		global::Gtk.Fixed.FixedChild w15 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.inputbox_label]));
		w15.X = 21;
		w15.Y = 293;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.total_txt_label = new global::Gtk.Label();
		this.total_txt_label.WidthRequest = 155;
		this.total_txt_label.HeightRequest = 54;
		this.total_txt_label.Name = "total_txt_label";
		this.total_txt_label.LabelProp = global::Mono.Unix.Catalog.GetString("label1");
		this.fixed1.Add(this.total_txt_label);
		global::Gtk.Fixed.FixedChild w16 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.total_txt_label]));
		w16.X = 18;
		w16.Y = 522;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.total_label = new global::Gtk.Label();
		this.total_label.WidthRequest = 148;
		this.total_label.HeightRequest = 48;
		this.total_label.Name = "total_label";
		this.total_label.LabelProp = global::Mono.Unix.Catalog.GetString("---------------");
		this.fixed1.Add(this.total_label);
		global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.total_label]));
		w17.X = 190;
		w17.Y = 526;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.undo_button = new global::Gtk.Button();
		this.undo_button.WidthRequest = 107;
		this.undo_button.HeightRequest = 55;
		this.undo_button.Sensitive = false;
		this.undo_button.CanFocus = true;
		this.undo_button.Name = "undo_button";
		this.undo_button.UseStock = true;
		this.undo_button.UseUnderline = true;
		this.undo_button.Label = "gtk-undo";
		this.fixed1.Add(this.undo_button);
		global::Gtk.Fixed.FixedChild w18 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.undo_button]));
		w18.X = 20;
		w18.Y = 642;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.apply_button = new global::Gtk.Button();
		this.apply_button.WidthRequest = 107;
		this.apply_button.HeightRequest = 55;
		this.apply_button.CanFocus = true;
		this.apply_button.Name = "apply_button";
		this.apply_button.UseStock = true;
		this.apply_button.UseUnderline = true;
		this.apply_button.Label = "gtk-save";
		this.fixed1.Add(this.apply_button);
		global::Gtk.Fixed.FixedChild w19 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.apply_button]));
		w19.X = 314;
		w19.Y = 642;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.button1 = new global::Gtk.Button();
		this.button1.WidthRequest = 160;
		this.button1.HeightRequest = 55;
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString("목록 보기");
		this.fixed1.Add(this.button1);
		global::Gtk.Fixed.FixedChild w20 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.button1]));
		w20.X = 140;
		w20.Y = 642;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.date_entry = new global::Gtk.Entry();
		this.date_entry.CanFocus = true;
		this.date_entry.Name = "date_entry";
		this.date_entry.IsEditable = true;
		this.date_entry.InvisibleChar = '●';
		this.fixed1.Add(this.date_entry);
		global::Gtk.Fixed.FixedChild w21 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.date_entry]));
		w21.X = 27;
		w21.Y = 142;
		this.Add(this.fixed1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 1252;
		this.DefaultHeight = 708;
		this.memo_entry.Hide();
		this.memobox_label.Hide();
		this.inputbox_label.Hide();
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.calendar3.DaySelected += new global::System.EventHandler(this.DaySelectedEvent);
		this.add_button.Clicked += new global::System.EventHandler(this.AddButtonClickedEvent);
		this.chore_box.Changed += new global::System.EventHandler(this.ComboBoxChangedEvent);
		this.price_entry.Changed += new global::System.EventHandler(this.ChangedEvent);
		this.memo_entry.KeyReleaseEvent += new global::Gtk.KeyReleaseEventHandler(this.MemoEntryKeyReleaseEvent);
		this.undo_button.Clicked += new global::System.EventHandler(this.UndoButtonClickedEvent);
		this.apply_button.Clicked += new global::System.EventHandler(this.ButtonClickedEvent);
		this.button1.Clicked += new global::System.EventHandler(this.ListViewButtonClickedEvent);
	}
}
