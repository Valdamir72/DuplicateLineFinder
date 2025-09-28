# DuplicateLineFinder
---
Утилита для анализа и очистки текстовых и структурированных (XML) файлов.

---

## Описание

### Цель создания

**Duplicate and Comment Finder** — это узкоспециализированный инструмент, разработанный для решения задач, с которыми не справляются стандартные текстовые редакторы. Основная цель — глубокий анализ и оптимизация больших, структурированных файлов, в частности XML-словарей формата DLMS/COSEM, где построчный поиск дубликатов неэффективен и приводит к большому количеству "шума".

Приложение позволяет безопасно удалять избыточные данные, что приводит к уменьшению размера файла и оптимизации его структуры.

### Ключевые возможности

1.  **Структурный анализ XML**
    *   Находит и группирует полностью идентичные блоки `<object>...</object>`.
    *   В качестве идентификатора группы отображается только короткий открывающий тег `<object ...>`, что делает интерфейс читаемым.
    *   Позволяет выборочно удалять целые XML-блоки, а не отдельные строки.

2.  **Текстовый анализ (для любых файлов)**
    *   **Поиск дубликатов строк:** Находит и группирует абсолютно одинаковые строки.
    *   **Поиск комментариев:** Находит все строки, содержащие однострочные комментарии в стиле C# (`//`).
    *   **Поиск пустых строк:** Находит все пустые или состоящие из пробельных символов строки.

3.  **Интерактивное управление**
    *   **Вкладки:** Результаты каждого типа анализа отображаются на отдельной, удобной вкладке.
    *   **Массовое выделение:** Контекстные меню на каждой вкладке позволяют мгновенно "Выделить всё" или "Снять выделение со всех" для быстрой работы с большими объемами данных.
    *   **Интерактивное дерево:** Для дубликатов используется `TreeView`, которое позволяет сворачивать и разворачивать группы.

4.  **Безопасное сохранение**
    *   При первом сохранении изменений создается нетронутая резервная копия оригинального файла (`имя_файла_orig.bak`).
    *   При каждом последующем сохранении обновляется "обычная" резервная копия (`имя_файла.bak`).

5.  **Экспорт отчетов**
    *   Позволяет экспортировать результаты анализа с текущей активной вкладки в один из двух форматов:
        *   **`.txt`** — для простого, человекочитаемого отчета.
        *   **`.json`** — для структурированного, машиночитаемого отчета.

### Технологический стек

*   **Язык программирования:** C#
*   **Платформа:** .NET 8
*   **Пользовательский интерфейс:** Windows Forms (WinForms)
*   **Основные используемые библиотеки (стандартные, входят в .NET SDK):**
    *   `System.IO` — для всех файловых операций.
    *   `System.Linq` — для группировки и обработки коллекций данных.
    *   `System.Xml.Linq` — для парсинга и манипуляции XML-документами (`XDocument`).
    *   `System.Text.Json` — для сериализации отчетов в формат JSON.

*Все используемые библиотеки являются частью .NET SDK и не требуют установки внешних NuGet-пакетов.*

---

## Description

### Purpose

**Duplicate and Comment Finder** is a highly specialized tool designed to solve problems that standard text editors cannot handle. Its primary purpose is the deep analysis and optimization of large, structured files, particularly XML dictionaries in the DLMS/COSEM format, where line-by-line duplicate searching is inefficient and generates significant "noise."

The application allows for the safe removal of redundant data, leading to a reduction in file size and an optimized structure.

### Key Features

1.  **Structural XML Analysis**
    *   Finds and groups completely identical `<object>...</object>` blocks.
    *   Displays only the short opening `<object ...>` tag as the group identifier, ensuring a readable interface.
    *   Allows for the selective deletion of entire XML blocks, rather than individual lines.

2.  **Text-based Analysis (for any file type)**
    *   **Duplicate Line Finder:** Finds and groups identical lines of text.
    *   **Comment Finder:** Locates all lines containing single-line C#-style comments (`//`).
    *   **Empty Line Finder:** Identifies all lines that are empty or consist only of whitespace.

3.  **Interactive Management**
    *   **Tabbed Interface:** The results of each analysis type are displayed in a separate, convenient tab.
    *   **Bulk Selection:** Context menus in each tab provide "Select All" and "Deselect All" options for quickly managing large volumes of data.
    *   **Interactive Tree View:** Duplicates are displayed in a `TreeView` that allows users to collapse and expand groups.

4.  **Safe Saving**
    *   Upon the first save, an untouched backup of the original file is created (`filename_orig.bak`).
    *   With each subsequent save, a regular backup is updated (`filename.bak`).

5.  **Report Exporting**
    *   Allows exporting the analysis results from the currently active tab into one of two formats:
        *   **`.txt`** — for a simple, human-readable report.
        *   **`.json`** — for a structured, machine-readable report.

### Tech Stack

*   **Programming Language:** C#
*   **Platform:** .NET 8
*   **User Interface:** Windows Forms (WinForms)
*   **Core Libraries Used (standard, included in the .NET SDK):**
    *   `System.IO` — for all file operations.
    *   `System.Linq` — for data collection grouping and processing.
    *   `System.Xml.Linq` — for parsing and manipulating XML documents (`XDocument`).
    *   `System.Text.Json` — for serializing reports into JSON format.

*All libraries used are part of the .NET SDK and do not require the installation of external NuGet packages.*
