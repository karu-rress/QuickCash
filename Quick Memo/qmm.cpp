#include <algorithm>
#include <chrono>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <memory>
#include <list>
#include <locale>
#include <sstream>

#include <cstring>
#include <format>

constexpr const char* VERSION{"1.1"};
constexpr bool is_64bit{sizeof(void*) == 8};

using uint32_pair = std::pair<std::uint32_t, std::uint32_t>;

namespace ocl {
    uint32_pair time_point_to_pair(std::chrono::system_clock::time_point&& t) {
        time_t time{std::chrono::system_clock::to_time_t(t)};
        tm *tm_{std::localtime(&time)};
        uint32_pair p{
            std::stoi(std::format("{:04d}{:02d}{:02d}", tm_->tm_year + 1900, tm_->tm_mon + 1, tm_->tm_mday)),
            std::stoi(std::format("{:02d}{:02d}{:02d}", tm_->tm_hour, tm_->tm_min, tm_->tm_sec))
        };

        return p;
    }

    [[gnu::always_inline]] inline void clear() { system("clear"); }
    [[gnu::always_inline]] inline void flush() { std::cin.ignore(128, '\n'); }
}

struct alignas(8) moment {
    moment() = default;
    explicit moment(uint32_pair &&p) noexcept : date{p.first}, time{p.second} {   }

    std::uint32_t date;
    std::uint32_t time;

    void operator<<(std::chrono::system_clock::time_point&& t) {
        std::tie(date, time) = ocl::time_point_to_pair(std::move(t));
    }
};


struct MemoData_ {
    uint32_t date;
    uint32_t time;
    std::string msg;
};

class MemoData {
public:
    static bool less(const MemoData &l, const MemoData &r) {
        return (l.GetDate() == r.GetDate())
            ? l.GetTime() < r.GetTime()
            : l.GetDate() < r.GetDate();
    }

    MemoData() noexcept {   }
    explicit MemoData(const char* msg) : MemoData{std::string{msg}} {  }
    explicit MemoData(const std::string &msg) : message{msg} { clock << std::chrono::system_clock::now(); }
    explicit MemoData(MemoData_ *r) : clock{uint32_pair{r->date, r->time}}, message{r->msg} {   }
    MemoData(const MemoData &rhs) : MemoData{rhs.message} {  }
    MemoData(MemoData &&r) noexcept : clock{r.clock}, message{std::move(r.message)} {  }

    [[nodiscard]] std::uint32_t GetDate() const noexcept { return clock.date; }
    [[nodiscard]] std::uint32_t GetTime() const noexcept { return clock.time; }

    [[nodiscard]] const moment & GetClock() const & noexcept { return clock; }
    [[nodiscard]] moment && GetClock() && noexcept { return std::move(clock); }

    [[nodiscard]] std::string&        get_msg() &         { return message; }
    [[nodiscard]] std::string&&       get_msg() &&        { return std::move(message); }
    [[nodiscard]] const std::string&  get_msg() const &   { return message; }

    bool operator==(const MemoData &m) const noexcept {
        return (clock.date == m.clock.date && clock.time == m.clock.time) ? true : false;
    }

    MemoData& operator=(const MemoData &rhs) {
        if (this != &rhs) {
            clock = rhs.clock;
            message = rhs.message;
        }
        return *this;
    }

    bool operator<(const MemoData &rhs) const noexcept {
        return (clock.date == rhs.clock.date)
            ? clock.time < rhs.clock.time
            : clock.date < rhs.clock.date;
    }

    MemoData& operator=(MemoData && rhs) {
        if (this != &rhs) {
            clock = rhs.clock;
            message = std::move(rhs.message);
        }
        return *this;
    }

private:
    moment clock;
    std::string message;
};


class UI {
public:
    explicit UI(std::list<MemoData> &m) : memo{m}, files{0U}, shots{1U} {   }

    void Read() {
        std::ifstream fin(savepath, std::ios_base::in);

        if (fin.is_open() == false) {
            return;
        }

        std::istringstream iss;
        MemoData_  m;

        for (; std::getline(fin, strbuf); iss.clear()) {
            iss.str(strbuf);

            iss >> m.date >> m.time;
            iss.ignore(1, ' ');
            std::getline(iss, m.msg);

            memo.emplace_back(m);
        }
        fin.close();
    }

    void PrintMenu() {
        ocl::clear();

        std::cout << "Date\t\tTime\t\tMessage" << std::endl << std::endl;
        memo.sort(MemoData::less);

        auto m = memo.cbegin();
        for (std::size_t i{1UL}; i <= memo.size(); ++i, ++m) {
            if (((i - 1) >= (shots + (shots * 19)) - 20) && ((i - 1) < shots * 19))
                std::cout << std::format("{}\t{}\t\t{}", m->GetDate(), m->GetTime(), m->get_msg()) << std::endl;
        }

        std::printf("\033[%d;%df", 23, 0);
        std::cout << std::flush << "[A]dd\t[R]emove\t[N]ext\t[P]rev\t[Q]uit" << std::endl;
        std::cout << ">>";
    }

    [[gnu::always_inline]] inline int Upper(char ch) { return (ch >= 'a' && ch <= 'z') ? ch - 32 : ch; }

    [[noreturn]] void Run() {
        char ch;
        for (Read(); true;) {
            PrintMenu();
            switch (std::cin >> ch; Upper(ch)) {
                case 'A': Add(); break;
                case 'R': Remove(); break;
                case 'N': if (!(shots * 20U >= memo.size())) ++shots; break;
                case 'P': if (shots > 1U) --shots; break;
                case 'Q': Save(); ocl::clear(); exit(0); break;
            }
        }
    }

    void Add() {
        ocl::clear();
        ocl::flush();

        std::cout << "Input a string: " << std::endl;
        std::getline(std::cin, strbuf);

        MemoData m_{strbuf};

        memo.remove_if([m_](const MemoData &m)->bool{ return m.GetDate() == m_.GetDate() && m.GetTime() == m_.GetTime();});
        memo.push_back(m_);
    }

    void Remove() {
        std::uint32_t n{0U}, n_{0U};
        char ch;

        ocl::clear();
        ocl::flush();

        std::cout << R"(Remove with:)
    [D]ate only
    Date with [T]ime
    [S]tring
    [R]emove all
>> )";


        switch (std::cin >> ch; Upper(ch)) {
            case 'D':
                ocl::flush();
                std::cout << "Input date: ";
                std::cin >> n;
                memo.remove_if([=](const MemoData &m) { return m.GetDate() == n; });
                return;
            case 'T':
                ocl::flush();
                std::cout << "Input date: ";
                std::cin >> n;
                ocl::flush();
                std::cout << "Input time: ";
                std::cin >> n_;
                memo.remove_if([=](const MemoData &m) { return m.GetDate() == n && m.GetTime() == n_; });
                return;
            case 'S':

                ocl::flush();
                std::cout << "Input string: ";
                std::cin >> strbuf;
                memo.remove_if([*this](const MemoData &m) { return m.get_msg() == this->strbuf; });
                return;
            case 'R':
                ocl::flush();
                memo.clear();
                return;
        }
    }

    void Save() {
        std::ofstream fout(savepath, std::ios_base::out);

        for (const auto &m : this->memo) {
            std::string str = std::format("{} {} {}", m.GetDate(), m.GetTime(), m.get_msg().data());
            fout << str << std::endl;
            std::cout << str << std::endl;
        }
        fout.close();
    }

private:
    constexpr static const char* savepath = "/home/sunwoo/qmm/data.qmm";
    std::list<MemoData> &memo;
    std::uint32_t files;
    std::uint32_t shots;
    std::string strbuf;
};

[[noreturn]] void print_version();
[[noreturn]] void wrong_args();
[[noreturn]] void arg(const char *msg, int n = 0);
void add(const char* const msg);
[[noreturn]] void help();
[[noreturn]] void run();

int main(int argc, char* argv[]) {
    if (argc == 1) {
        run();
    }
    switch (argc) {
        case 2:
            if (!std::strcmp(argv[1], "version") || !std::strcmp(argv[1], "-v") || !std::strcmp(argv[1], "--version"))
                print_version();
            else if (!std::strcmp(argv[1], "clear") || !std::strcmp(argv[1], "-c"))
                arg(R"(Removed. Try "echo $?".)", remove("/home/sunwoo/qmm/data.qmm"));
            else if (!std::strcmp(argv[1], "help"))
                help();
            else
                wrong_args();
            break;
        case 3:
            if (!std::strcmp(argv[1], "add") || !std::strcmp(argv[1], "-a")) {
                add(argv[2]);
                arg("Memo has been added successfully.");
                break;
            }
            [[fallthrough]];
        default:
            wrong_args();
    }
}

[[noreturn]] void run() {
    std::list<MemoData> memo;
    UI ui{memo};
    ui.Run();
}

[[noreturn]] void print_version() {
    std::cout << "qmm version " << VERSION << std::endl
        << "copyright 2017 Sunwoo Na. Over rights reserved." << std::endl;
    exit(0);
}

[[noreturn]] void wrong_args() {
    std::cout << "qmm: arguments are wrong." << std::endl;
    exit(1);
}

[[noreturn]] void arg(const char *msg, int n /* = 0 */) {
    std::cout << msg << std::endl;
    exit(n);
}

void add(const char* const msg) {
    std::chrono::system_clock::time_point t = std::chrono::system_clock::now();
    std::ofstream fout("/home/sunwoo/qmm/data.qmm", std::ios_base::app);
    auto [date, time] = ocl::time_point_to_pair(std::move(t));

    fout << std::format("{} {} {}", date, time, msg) << std::endl;
    fout.close();
}

[[noreturn]] void help() {
    std::cout <<
R"(qmm - The quickest memo program

Usage:

qmm (optional)[arg] (optional)[args...]

qmm - Run qmm
qmm [arg] - Run qmm, but not open main window

arg:

help - Prints guide
version(-v, --version) - Prints current version
clear(-c) - Clear all data
add(arg 2 is needed, -a) - Add arg 2 without open main window


copyright 2017 Sunwoo Na. Over rights reserved.)" << std::endl;
    exit(0);
}
