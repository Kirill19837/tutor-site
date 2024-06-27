document.addEventListener("DOMContentLoaded", function () {
    // ������ ������� � ������ material_article
    var materialArticle = document.querySelector('.material_article');

    // ���������, �� ���� �������
    if (materialArticle) {
        // ������� ��� � �������� description-data, tags-data �� title-data
        var description = materialArticle.getAttribute('description-data');
        var tags = materialArticle.getAttribute('tags-data');
        var title = materialArticle.getAttribute('title-data');

        // ���� description ������, ��������������� title-data
        if (!description) {
            description = title;
        }

        // ������ ��� �������� ����-��� description
        var metaDescription = document.querySelector('meta[name="description"]');
        if (metaDescription) {
            metaDescription.setAttribute("content", description);
        } else {
            var newMetaDescription = document.createElement('meta');
            newMetaDescription.setAttribute("name", "description");
            newMetaDescription.setAttribute("content", description);
            document.head.appendChild(newMetaDescription);
        }

        // �������� title-data �� ����� �� ������ �� tags
        if (title) {
            tags += ", " + title.split(' ').join(', ');
        }

        // ������ ��� �������� ����-��� keywords
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