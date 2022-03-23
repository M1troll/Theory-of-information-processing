import tkinter as tk
from tkinter.messagebox import showerror, askokcancel, showinfo
import re
from functools import reduce


class Code:
    window = tk.Tk()                        # Окно приложения
    window.title("Код Хэмминга")            # Заголовок приложения
    window.iconbitmap(default='ico.ico')    # Иконка приложения
    info = tk.StringVar()                   # Строка состояния

    X_COUNT = 11                            # Константа кол-ва бит в исходном пакете
    Y_COUNT = 4                             # Константа кол-ва контрольных бит
    BIT_COUNT = 15                          # Константа общего числа бит зашифрованной последовательности
    ERROR_FLAG = False                      # Константа существования ошибки в шифре

    def __init__(self):
        self.package_buttons = []
        self.package_buttons_opened = []

        # Создаем список кнопок-бит пакета по умолчанию
        for x in range(Code.X_COUNT):
            button = tk.Button(self.window, width=3, text=0,
                               background="lightsteelblue",
                               activebackground="cornflowerblue")
            button.config(command=lambda btn=button: self.change_code(btn))
            self.package_buttons.append(button)

        y_num = 1
        x_num = 1

        # Создаем список кнопок-бит шифра по умолчанию
        for b in range(Code.BIT_COUNT):

            # Проверяем, является ли текущий индекс степенью двойки:
            if bin(b + 1).count('1') == 1 or bin(b).count('1') == 0:
                # Если индекс является степенью двойки, выделяем контрольный параметр
                button_open = tk.Button(self.window, width=3, background="lightgreen")
                button_open.widgetName = f"y{y_num}"
                y_num += 1
            else:  # Иначе добавляем обычную кнопку
                button_open = tk.Button(self.window, width=3, text=0, background="lightsteelblue")
                button_open.widgetName = f"x{x_num}"
                x_num += 1

            # Назначаем метод отметки ошибки на кнопки открытого списка кнопок-бит шифра
            button_open.config(command=lambda btn=button_open: self.mark_mistake(btn))
            self.package_buttons_opened.append(button_open)

    @staticmethod
    def change_bit(btn: tk.Button):
        """
        Меняет бит кнопки с 1 на 0 и с 0 на 1.

        :param btn: Кнопка, которой нужно изменить код.
        :return: None
        """

        if btn['text']:
            btn['text'] = 0
        else:
            btn['text'] = 1

    @staticmethod
    def return_color(btn: tk.Button):
        """
        Возвращает цвет кнопки при отмене метки ошибки.

        :param btn: Кнопка, которой нужно вернуть цвет.
        :return: None
        """

        if re.fullmatch(r'y.', btn.widgetName):
            btn['background'] = 'lightgreen'
        else:
            btn['background'] = "lightsteelblue"

    @staticmethod
    def split_into_segments(data, n):
        """
        Извлекает последовательность: начиная с n каждые n через n

        :param data: Список, из которого нужно извлечь последовательность.
        :param n: Индекс последовательности.
        :return: Последовательность вида "начиная с n каждые n через n".
        """

        segment = []
        i = n - 1

        while True:
            try:
                for _ in range(n):
                    segment.append(data[i])
                    i += 1
                i += n
            except IndexError:
                break

        return segment

    @staticmethod
    def power_of_two(n):
        """
        Находит ближайшую степень двойки к числу, не превосходящую его.

        :param n: Число, ближайшую степень двойки к которому нужно найти.
        :return: Ближайшую степень двойки к числу n.
        """

        power = 1
        while 2 ** power < n:
            power += 1

        return power - 1

    def change_code(self, btn: tk.Button):
        """
        Изменяет код кнопки шифров

        :param btn: Кнопка-бит, нажатая в исходном коде.
        :return: None
        """

        self.change_bit(btn)  # Меняем бит кнопки
        index = self.package_buttons.index(btn) + 1  # Получаем индекс кнопки шифра

        # Меняем бит в открытом шифре
        open_code_btn = [b for b in self.package_buttons_opened if b.widgetName == f"x{index}"][0]
        open_code_btn['text'] = btn['text']

        # Снимаем метку ошибки при изменении исходного пакета
        self.return_color(open_code_btn)

    def mark_mistake(self, btn: tk.Button):
        """
        Помечает красным цветом ошибку и контролирует, чтобы их не было больше одной.

        :param btn: Кнопка, на которую нажали, чтобы пометить ошибку.
        :return: None
        """

        red_btn = [b for b in self.package_buttons_opened if b['background'] == 'red']

        if not red_btn:                         # Если пометку ещё нет
            btn['background'] = 'red'           # Помечаем кнопку
            self.change_bit(btn)                # Меняем бит кнопки
            Code.ERROR_FLAG = True              # Фиксируем факт наличия ошибки
        else:
            if btn['background'] == 'red':      # Если метку уже стоит на нажатой кнопке
                self.return_color(btn)          # Убираем с неё метку
                Code.ERROR_FLAG = False         # Фиксируем факт отсутствия ошибки
            else:                               # Если метка уже стоит на другой кнопке
                self.return_color(red_btn[0])   # Убираем метку с другой кнопки
                self.change_bit(red_btn[0])     # Меняем бит другой кнопки
                btn['background'] = 'red'       # И ставим её на нажатую кнопку

            self.change_bit(btn)  # Меняем бит нажатой кнопки

    def generate_test(self):
        """
        Заполняет исходный код пакета тестовым примером.

        :return: None
        """

        # Меняем кол-во исходных бит и бит шифра
        Code.X_COUNT = 16
        Code.Y_COUNT = 5
        Code.BIT_COUNT = 21

        # Обновляем интерфейс программы
        self.reload()

        # Инвертированный исходный код [0100010000111101]
        test = [1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0]

        # # Пробегаем по списку кнопок-бит исходного пакета и тестового кода
        for btn, bit in zip(self.package_buttons, test):
            # Задаем значение бита и принудительно обновляем его, чтобы инвертировать
            btn['text'] = bit
            self.change_code(btn)

    def create_widgets(self):
        """
        Создает все виджеты приложения

        :return: None
        """

        menubar = tk.Menu(self.window)
        self.window.config(menu=menubar)

        settings_menu = tk.Menu(menubar, tearoff=0)
        settings_menu.add_command(label="Выполнить", command=self.make_calculate)
        settings_menu.add_command(label="Очистить", command=self.reload)
        settings_menu.add_command(label="Пример", command=self.generate_test)
        settings_menu.add_command(label="Настройки", command=self.create_settings_win)
        settings_menu.add_command(label="Выход", command=self.window.destroy)
        menubar.add_cascade(label="Возможности", menu=settings_menu)

        tk.Label(text="Биты исходного пакета").grid(row=0,
                                                    column=0,
                                                    columnspan=Code.BIT_COUNT,
                                                    padx=15, pady=15)

        column = 0
        for b in self.package_buttons:
            b.grid(row=1, column=column, padx=2, pady=2)
            column += 1

        tk.Label(text="Результат шифрования").grid(row=2,
                                                   column=0,
                                                   columnspan=Code.BIT_COUNT,
                                                   padx=15, pady=15)
        column = 0
        for b in self.package_buttons_opened:
            b.grid(row=3, column=column, padx=2, pady=2)
            column += 1

        calculate_btn = tk.Button(self.window, text="Вычислить", command=self.make_calculate)
        calculate_btn.grid(row=4, column=0, columnspan=3, padx=5, pady=25)

        find_mistake_btn = tk.Button(self.window, text="Найти ошибку", command=self.find_mistake)
        find_mistake_btn.grid(row=4, column=3, columnspan=4, padx=5, pady=25)

        tk.Label(textvariable=self.info, foreground='red').grid(row=5, column=0, columnspan=6, padx=5, pady=5)

    def find_mistake(self):
        """
        Осуществляет поиск бита, в котором допущена ошибка (если она допущена)

        :return: Сообщение с номером бита, в котором допущена ошибка.
        """
        
        index_bit_error = 0                             # Переменная для хранения номера бита с ошибкой
        control_bits = self.calculate_control_bits()    # Позиции и значения контрольных бит в текущий момент
        keys = control_bits.keys()                      # Позиции контрольных бит
        values = control_bits.values()                  # Значения контрольных бит

        # Получаем значения y, "пришедшие" в шифре
        yy = [int(btn_y['text']) for btn_y in self.package_buttons_opened
              if self.package_buttons_opened.index(btn_y) in keys]

        # Выводим в строку состояния исходные значения контрольных бит
        s = "Исходные контрольные биты: "
        for y in yy:
            s += f"{y}"

        # Выводим в строку состояния текущие значения контрольных бит
        s += "\n  Текущие контрольные биты: "
        for y in values:
            s += f"{y}"

        self.info.set(s)

        # Сравниваем списки контрольных бит
        # reduce - применяет переданную функцию к каждому элементу итерируемого ввода рекурсивным образом
        # map - принимает в качестве аргументов функцию и итерацию;
        # применяет переданную функцию к каждому элементу итерации, а затем возвращает итератор, в качестве результата.

        if reduce(lambda x, z: x and z, map(lambda p, q: p == q, values, yy), True):
            showinfo("Всё в порядке", "Шифр не содержит ошибок!")
            return
        else:
            for (key, value), y in zip(control_bits.items(), yy):
                if value != y:
                    index_bit_error += key + 1

        # Выводим сообщение с номером ошибочного бита
        answer = askokcancel("Шифр пришёл с ошибкой!",
                             f"Бит под №{index_bit_error} пришёл с ошибкой.\nЖелаете инвертировать его?")
        if answer:
            # Если пользователь нажмет да, ошибочный код инвертируется в исходный, и удалится метка ошибки
            self.change_bit(self.package_buttons_opened[index_bit_error-1])
            self.return_color(self.package_buttons_opened[index_bit_error-1])
            Code.ERROR_FLAG = False

        # Возвращаем его значение
        return index_bit_error

    def start(self):
        """
        Запускает программу

        :return: None
        """

        self.create_widgets()
        Code.window.mainloop()

    def reload(self):
        """
        Перезапускает программу

        :return: None
        """

        [child.destroy() for child in self.window.winfo_children()]  # Удаляем все дочерние виджеты

        self.__init__()
        self.create_widgets()

    def make_calculate(self):
        """
        Осуществляет подсчет контрольных бит и их визуализацию.

        :return: None
        """

        # Проверяем наличие ошибок в шифре
        if Code.ERROR_FLAG:
            # Если ошибки есть, то запрещаем выполнять формирование шифра
            showinfo("Допущена ошибка!", "В коде шифра отмечен бит-ошибка. Формирование шифра не возможно.")
        else:
            # Получаем позицию и значение контрольных бит
            control_bits = self.calculate_control_bits()
            for key, value in control_bits.items():
                # Записываем значение контрольного бита в открытый список кнопок-бит шифра
                self.package_buttons_opened[key]['text'] = value
                self.return_color(self.package_buttons_opened[key])  # Возвращаем цвет кнопки

    def calculate_control_bits(self):
        """
        Вычисляет контрольные биты для шифра пакета.

        :return: Словарь позиций и значений контрольных бит.
        """

        control_bits = dict()  # Словарь для хранения позиции контрольного бита и его значения
        yy = [2 ** i for i in
              range(Code.Y_COUNT)]  # Получаем список всех степеней двойки, занимаемых контрольными битами

        # Для каждого контрольного бита
        for n in yy:
            # Вычисляем последовательность: каждые n бит через каждые n бит, начиная с n-го бита
            segment = self.split_into_segments(self.package_buttons_opened, n)

            # Вычисляем сумму подконтрольных бит последовательности
            bit_sum = 0
            for i in range(1, len(segment)):
                bit_sum += int(segment[i]['text'])

            # Если сумма нечетная, то контрольный бит равен единице
            if bit_sum % 2 == 1:
                control_bits[n - 1] = 1
            else:
                # Если же сумма четная, то контрольный бит равен нулю
                control_bits[n - 1] = 0

        return control_bits

    def create_settings_win(self):
        """
        Создает окно настроек программы

        :return: None
        """

        win_settings = tk.Toplevel(self.window)
        win_settings.wm_title("Настройки")

        tk.Label(win_settings, text="Количество бит в исходном пакете").grid(row=0, column=0)
        x_entry = tk.Entry(win_settings)
        x_entry.insert(0, str(Code.X_COUNT))
        x_entry.grid(row=0, column=1, padx=20, pady=20)

        tk.Label(win_settings, text="Количество контрольных бит в шифре", state='disabled').grid(row=1, column=0)
        y_entry = tk.Entry(win_settings)
        y_entry.insert(0, str(Code.Y_COUNT))
        y_entry["state"] = 'disabled'
        y_entry.grid(row=1, column=1, padx=20, pady=20)

        tk.Label(win_settings, text="Итоговое количество бит в шифре", state='disabled').grid(row=2, column=0)
        b_entry = tk.Entry(win_settings)
        b_entry.insert(0, str(Code.BIT_COUNT))
        b_entry["state"] = 'disabled'
        b_entry.grid(row=2, column=1, padx=20, pady=20)

        save_btn = tk.Button(win_settings, text="Применить",
                             command=lambda win=win_settings: self.change_settings(x_entry, win))
        save_btn.grid(row=3, column=0, columnspan=2, padx=20, pady=20)

        # Делаем окно настроек модальным
        win_settings.grab_set()     # Не дает возвращаться к родительскому окну, пока не закрыто дочернее
        win_settings.focus_set()    # Переводит фокус на это самое дочернее окно, чтобы по нему не кликать лишний раз

    def change_settings(self, row: tk.Entry, win: tk.Toplevel):
        """
        Проверяет корректность изменений настроек


        :param win: Окно настроек.
        :param row: Поле с измененным параметром.
        :return: None
        """

        try:
            num = int(row.get())
            if num <= 0:
                raise ValueError
        except ValueError:
            showerror("Ошибка!", "Вы ввели некорректное значение!")
            return

        Code.X_COUNT = num
        Code.Y_COUNT = 2  # На единственный бит приходится 2 контрольных

        if num == 2:
            Code.Y_COUNT += 1  # Частный случай шифра из двух бит - 3 контрольных бита

        # В ином случае количество контрольных бит соответствует 2 + степень двойки, ближайшая к числу исходных бит
        Code.Y_COUNT += self.power_of_two(int(row.get()))

        Code.BIT_COUNT = Code.X_COUNT + Code.Y_COUNT

        self.reload()
        win.destroy()  # Закрываем окно настроек


if __name__ == "__main__":
    code = Code()
    code.start()
