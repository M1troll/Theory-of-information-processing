from PIL import Image as Img    # Класс для работы с изображением
from PIL import ImageDraw       # Библиотека для модификации изображений

from kivy.app import App
from kivy.uix.button import Button
from kivy.uix.image import Image
from kivy.uix.label import Label
from kivy.uix.textinput import TextInput

from kivy.uix.boxlayout import BoxLayout
from kivy.uix.anchorlayout import AnchorLayout
from kivy.uix.gridlayout import GridLayout

from kivy.core.window import Window
from kivy.config import Config                # Импортируем модуль настроек
Config.set("graphics", "resizable", "0")      # запрещаем изменение размера окна
Config.set("graphics", "width", "320")        # Фиксируем ширину
Config.set("graphics", "height", "240")       # Фиксируем высот


class MyTextInput(TextInput):
    def __init__(self, **kwargs):
        super(MyTextInput, self).__init__(**kwargs)
        self.input_filter = 'int'                       # Разрешаем ввод только цифр

    def _set_text(self, text):
        super(MyTextInput, self)._set_text(text)
        self.text = text


class MobileApp(App):

    def __init__(self):
        super(MobileApp, self).__init__()

        path = "image.jpg"
        self.main_image = Image(source=path, size_hint=[.89, .99])
        self.image = Img.open(path)                                 # Открываем изображение
        self.draw = ImageDraw.Draw(self.image)                      # Создаем инструмент для рисования
        self.width = self.image.size[0]                             # Определяем ширину
        self.height = self.image.size[1]                            # Определяем высоту
        self.pix = self.image.load()                                # Выгружаем значения пикселей

        self.matrix_filter = []                                     # Массив для хранения матричного фильтра
        for i in range(0, 3):
            row = []
            for j in range(0, 3):
                row.append(MyTextInput(size=(1, 1)))
                # row[j].text = int(1)
            self.matrix_filter.append(row)

    def build(self):
        """
        Осуществляет построение приложения

        :return: Структуру, хранящую все виджеты, которые нужно отрисовать
        """

        # Создание слоев
        al = AnchorLayout(anchor_x='center',
                          anchor_y='center',
                          padding=[20])          # Расстояние виджетов от других объектов (Left, Up, Right, Down)
        bl1 = BoxLayout(orientation="vertical",
                        size_hint=[.8, .8],
                        spacing=2)               # Расстояние между объектами
        bl2 = BoxLayout(orientation="vertical",
                        size_hint=[.8, .8],
                        spacing=2)               # Расстояние между объектами
        bl_main = BoxLayout(orientation="horizontal",
                            size_hint=[.8, .8],
                            spacing=2)
        gl_matrix = GridLayout(cols=3,           # Слой хранения матричного фильтра
                               rows=4,
                               spacing=2)        # Расстояние между объектами

        # Левый столбец
        bl1.add_widget(self.main_image)
        bl1.add_widget(Button(text=f"Негатив",
                              font_size=20,
                              on_press=self.make_negative,
                              background_color=[.31, .2, .38, 1],  # %RGBA
                              background_normal='',
                              size_hint=[.8, .20]
                              ))
        bl1.add_widget(Button(text=f"Оттенки серого",
                              font_size=20,
                              on_press=self.make_grey,
                              background_color=[.31, .2, .38, 1],  # %RGBA
                              background_normal='',
                              size_hint=[.8, .20]
                              ))
        bl1.add_widget(Button(text=f"Откат",
                              font_size=20,
                              on_press=self.rollback,
                              background_color=[.72, .15, .15, 1],  # %RGBA
                              background_normal='',
                              size_hint=[.8, .20]
                              ))

        # Правый столбец
        bl2.add_widget(Label(text="Введите матричный фильтр"))

        for row in self.matrix_filter:
            for text_input in row:
                gl_matrix.add_widget(text_input)

        bl2.add_widget(gl_matrix)

        bl2.add_widget(Button(text=f"Применить",
                              font_size=20,
                              on_press=self.apply_filter,
                              background_color=[0, .65, .13, 1],  # %RGBA
                              background_normal='',
                              size_hint=[.7, .2]
                              ))

        # Добавление слоев
        bl_main.add_widget(bl1)
        bl_main.add_widget(bl2)
        al.add_widget(bl_main)

        return al

    def make_negative(self, instance):
        """
        Преображает пиксели изображения в негативные цвета

        :return: None
        """

        print("Преобразование в негатив")   # Сообщение в консоль

        for w in range(self.width):
            for h in range(self.height):
                a = self.pix[w, h][0]
                b = self.pix[w, h][1]
                c = self.pix[w, h][2]
                self.draw.point((w, h), (255 - a, 255 - b, 255 - c))

        path = "new_image.jpg"
        self.image.save(path, "JPEG")       # Сохраняем результат
        self.save_changes(path)             # Загружаем изменения изображения

    def make_grey(self, instance):
        """
        Преображает пиксели изображения в оттенки серого

        :return: None
        """

        print("Преобразование в серый")     # Сообщение в консоль

        for i in range(self.width):
            for j in range(self.height):
                a = self.pix[i, j][0]
                b = self.pix[i, j][1]
                c = self.pix[i, j][2]
                abc = (a + b + c) // 3
                self.draw.point((i, j), (abc, abc, abc))

        path = "new_image.jpg"
        self.image.save(path, "JPEG")       # Сохраняем результат
        self.save_changes(path)             # Загружаем изменения изображения

    @staticmethod
    def close_app():
        """
        Завершает работу приложения

        :return: None
        """

        Window.close()

    def save_changes(self, path):
        """
        Обновляет изображение после изменений

        :return: None
        """

        self.image = Img.open(path)             # Открываем изображение
        self.pix = self.image.load()            # Выгружаем значения пикселей
        self.draw = ImageDraw.Draw(self.image)  # Создаем инструмент для рисования
        self.main_image.source = path           # Обновляем главное изображение
        self.main_image.reload()                # Перезагружаем изображение

    def rollback(self, instance):
        """
        Возвращает изображение к исходному виду

        :return: None
        """

        print("Откат к исходному представлению")  # Сообщение в консоль

        path = "image.jpg"
        self.save_changes(path)                   # Загружаем изменения изображения

    def apply_filter(self, instance):
        """
        Преобразует изображение по матричному фильтру

        :param instance: Кнопка, по нажатию которой запустился метод.
        :return: None
        """

        print("Применение матричного фильтра")  # Сообщение в консоль

        matrix = []
        sum_matrix = 0
        for row in self.matrix_filter:
            num_row = []
            for text_input in row:
                if text_input.text == '':
                    num = 1                     # Если одна из ячеек пустая, то заменяем её единицей
                else:
                    num = int(text_input.text)

                sum_matrix += abs(num)          # Получаем сумму чисел по модулю
                num_row.append(num)
            matrix.append(num_row)

        if sum_matrix == 0:
            sum_matrix += 1

        fake_image = self.image.copy()      # Временный клон изображения
        draw = ImageDraw.Draw(fake_image)   # Создаем инструмент для рисования

        for w in range(1, self.width - 1):
            for h in range(1, self.height - 1):
                # Первая строка
                a1 = self.pix[w - 1, h - 1][0] * matrix[0][0]
                b1 = self.pix[w - 1, h - 1][1] * matrix[0][0]
                c1 = self.pix[w - 1, h - 1][2] * matrix[0][0]
                a2 = self.pix[w, h - 1][0] * matrix[0][1]
                b2 = self.pix[w, h - 1][1] * matrix[0][1]
                c2 = self.pix[w, h - 1][2] * matrix[0][1]
                a3 = self.pix[w + 1, h - 1][0] * matrix[0][2]
                b3 = self.pix[w + 1, h - 1][1] * matrix[0][2]
                c3 = self.pix[w + 1, h - 1][2] * matrix[0][2]

                # Вторая строка
                a4 = self.pix[w - 1, h][0] * matrix[1][0]
                b4 = self.pix[w - 1, h][1] * matrix[1][0]
                c4 = self.pix[w - 1, h][2] * matrix[1][0]
                a5 = self.pix[w, h][0] * matrix[1][1]           # Главный пиксель
                b5 = self.pix[w, h][1] * matrix[1][1]           # Главный пиксель
                c5 = self.pix[w, h][2] * matrix[1][1]           # Главный пиксель
                a6 = self.pix[w + 1, h][0] * matrix[1][2]
                b6 = self.pix[w + 1, h][1] * matrix[1][2]
                c6 = self.pix[w + 1, h][2] * matrix[1][2]

                # Третья строка
                a7 = self.pix[w - 1, h + 1][0] * matrix[2][0]
                b7 = self.pix[w - 1, h + 1][1] * matrix[2][0]
                c7 = self.pix[w - 1, h + 1][2] * matrix[2][0]
                a8 = self.pix[w, h + 1][0] * matrix[2][1]
                b8 = self.pix[w, h + 1][1] * matrix[2][1]
                c8 = self.pix[w, h + 1][2] * matrix[2][1]
                a9 = self.pix[w + 1, h + 1][0] * matrix[2][2]
                b9 = self.pix[w + 1, h + 1][1] * matrix[2][2]
                c9 = self.pix[w + 1, h + 1][2] * matrix[2][2]

                # Вычисление результата
                result = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9
                result += b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9
                result += c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9

                result = result//sum_matrix
                while result > 255 or result < 0:
                    if result > 255:
                        result -= 255
                    else:
                        result += 255

                # Запись получившегося пикселя
                draw.point((w, h), (255 - a1, 255 - b1, 255 - c1))

        path = "new_image.jpg"
        fake_image.save(path, "JPEG")  # Сохраняем результат
        self.save_changes(path)        # Загружаем изменения изображения


if __name__ == "__main__":
    MobileApp().run()
