document.addEventListener("DOMContentLoaded", function () {
    // Знайти елемент з класом material_article
    var materialArticle = document.querySelector('.material_article');

    // Перевірити, чи існує елемент
    if (materialArticle) {
        // Витягти дані з атрибутів description-data, tags-data та title-data
        var description = materialArticle.getAttribute('description-data');
        var tags = materialArticle.getAttribute('tags-data');
        var title = materialArticle.getAttribute('title-data');

        // Якщо description пустий, використовувати title-data
        if (!description) {
            description = title;
        }

        // Знайти або створити мета-тег description
        var metaDescription = document.querySelector('meta[name="description"]');
        if (metaDescription) {
            metaDescription.setAttribute("content", description);
        } else {
            var newMetaDescription = document.createElement('meta');
            newMetaDescription.setAttribute("name", "description");
            newMetaDescription.setAttribute("content", description);
            document.head.appendChild(newMetaDescription);
        }

        // Розділити title-data на слова та додати до tags
        if (title) {
            tags += ", " + title.split(' ').join(', ');
        }

        // Знайти або створити мета-тег keywords
        var metaKeywords = document.querySelector('meta[name="keywords"]');
        if (metaKeywords) {
            metaKeywords.setAttribute("content", tags);
        } else {
            var newMetaKeywords = document.createElement('meta');
            newMetaKeywords.setAttribute("name", "keywords");
            newMetaKeywords.setAttribute("content", tags);
            document.head.appendChild(newMetaKeywords);
        }
    }
});