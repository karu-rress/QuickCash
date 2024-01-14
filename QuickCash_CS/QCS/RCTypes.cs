//
// RCTypes.cs
//
#define KOREAN
#define BETA
#define LINUX

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gtk;
using ChoreType = System.Collections.Generic.Dictionary<string, int?>;
using MemoType = System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<(string chore, int? price)>>;
using RollingAttribute;

namespace RC
{
	[RollingAbout("Sunwoo Na"), Version(0.92), Language(Language.Korean)]
    public static class RollingData
    {
        public const string Title = "Rolling Cashier v0.92";
#if KOREAN
		// Chores(Datas)
        public const string Clean = "청소";
        public const string Recycle = "재활용";
        public const string LaundryLittle = "빨래(소)";
        public const string LaundryMuch = "빨래(대)";
        public const string Dishes = "설거지";

		// Program Labels
        public const char Won = '₩';
        public const string ProgramName = "Rolling Cashier";
        public const string Description = "Quick Cash with GUI";
        public const string SelectDay = "(날짜를 선택하세요)";
        public const string Empty = "비어있음";
        public const string Total = "합계";
        public const string InputSelf = "직접 입력";
        public const string Add = "추가 >>>";
        public const string InputPrice = "금액을 입력해 주세요!";
        public const string NormalMemo = "일반 메모";

		// Messages
        public const string ReadError = "저장된 데이터를 읽는 중 에러가 발생했습니다.\n데이터 없이 계속 하시겠습니까?\n('아니요'를 누르면 종료합니다.)";
        public const string TextChangedMessage = "금액 데이터가 변경되었습니다.\n이 설정으로 유지하시겠습니까?";
        public const string PriceInputError = "금액 입력란에는 숫자만 입력할 수 있습니다.";
        public const string TooBigPrice = "금액이 너무 큽니다.\n2147483647원 미만으로 입력해 주십시오";
        public const string ErrorMessage = "에러가 발생했습니다. \n에러 내용이 자동으로 전송되었으니,곧 해결 될 것입니다.\n불편을 드려 죄송합니다.";
#else
        // English..
#endif

		// If the macro 'BETA' defined....
#if BETA
        public const bool IsBeta = true;
#else
        public const bool IsBeta = false;
#endif
        public const float Version = 9.2e-1F;

#pragma warning disable RECS0110 // Condition is always 'true' or always 'false'
        public const string BETA = (IsBeta) ? "BETA" : null; // Either "BETA" or null...
#pragma warning restore RECS0110

#if LINUX
        public static readonly string DataFilePath = $@"{Environment.GetEnvironmentVariable("HOME")}/.rc/rc.data";
        public static readonly string MapFilePath = $@"{Environment.GetEnvironmentVariable("HOME")}/.rc/rc.map";
#else
		// define a path for Windows or Mac OSX
#endif

		// static variables for the program
        public static readonly Calendar Calendar = new Calendar();
        public static ChoreType ChoreMap = new ChoreType();
        public static MemoType Memo = new MemoType();
        public static List<(string date, string chore, int? price)> MemoUndo = new List<(string, string, int?)>();

		// xxxx-xx-xx where x: digit
        public static readonly Regex DateRegex = new Regex(@"^\d{4}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$");
    }
}
