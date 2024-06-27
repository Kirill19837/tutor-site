document.addEventListener("DOMContentLoaded", function () {
    var materialArticle = document.querySelector('.material_article');

    if (materialArticle) {
        var description = materialArticle.getAttribute('description-data');
        var tags = materialArticle.getAttribute('tags-data');
        var title = materialArticle.getAttribute('title-data');

        if (!description) {
            description = title;
        }

        var metaDescription = document.querySelector('meta[name="description"]');
        if (metaDescription) {
            metaDescription.setAttribute("content", description);
        } else {
            var newMetaDescription = document.createElement('meta');
            newMetaDescription.setAttribute("name", "description");
            newMetaDescription.setAttribute("content", description);
            document.head.appendChild(newMetaDescription);
        }

        if (title) {
            tags += ", " + title.split(' ').join(', ');
        }

        var metaKeywords = document.querySelector('meta[name="keywords"]');
        if (metaKeywords) {
            metaKeywords.setAttribute("content", tags);
        } else {
            var newMetaKeywords = document.createElement('meta');
            newMetaKeywords.setAttribute("name", "keywords");
            newMetaKeywords.setAttribute("content", tags);
            document.head.appendChild(newMetaKeywords);
        }
    } else {
        console.log("Element with class 'material_article' not found.");
    }
});