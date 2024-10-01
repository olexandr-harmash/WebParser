# WebParser

Смотреть Program.cs

**Utils**
Файл содержит методы для вычисления TF IDF и вычисление косинуса угла между векторами.

**Document**
Файл содержит методы парсинга и токенизации слов и построение словаря -> (слово, кол. вхождений)

**Program**
Рассматривает 2 документа (статьи википедии). Выполняет вычисления  TF IDF для 1 документа, а также сравнение документов с помощь вычисления косинуса угла между векторами "схожести".

## Запуск программы

```bash
  git clone https://github.com/olexandr-harmash/WebParser.git
  cd ./WebParser
  dotnet run
```

**результат работы**
Для приведенных документов и слова в Program.cs результат будет таким.

```bash
  Term Frequency (TF) of 'ukrainian': 0.010414398722693955
  Inverse Document Frequency (IDF) of 'ukrainian': 0
  Cosine Similarity between documents: 0.8149539154275957
```
