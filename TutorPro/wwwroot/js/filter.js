
$(document).ready(function () {
    sendRequest();
        $('.filter-form__select').on('change', function () {
            sendRequest();
        });

        document.querySelector('.filter-form__input').addEventListener('input', function () {
            var searchValue = this.value;
            sendRequest("search", searchValue); // Передаємо нове значення searchValue в функцію sendRequest
        });

        $('.pagination__list').on('click', '.pagination__list-link', function (e) {
            e.preventDefault();
            var page = $(this).text(); // Отримайте номер сторінки
            sendRequest(null,null,page); // Відправте AJAX запит з номером сторінки
        });

});


// Функція для відправки AJAX запиту
function sendRequest(key = null, value = null, page = 1) {
    var subject = $('#subject').hasClass('new-select') && $('#subject').hasClass('empty') ?
        $('#subject').text().trim() : $('#subject').val();

    var grade = $('#grade').hasClass('new-select') && $('#grade').hasClass('empty') ?
        $('#grade').text().trim() : $('#grade').val();

    var level = $('#level').hasClass('new-select') && $('#level').hasClass('empty') ?
        $('#level').text().trim() : $('#level').val();

    var sort = $('#sort').hasClass('new-select') && $('#sort').hasClass('empty') ?
        $('#sort').text().trim() : $('#sort').val();

    var searchValue = document.getElementsByClassName('filter-form__input')[0].value

    // Перевірка, чи передано новий ключ і значення
    if (key !== null && value !== null) {
        // Додаємо новий ключ та його значення до об'єкту параметрів запиту
        switch (key) {
            case 'subject':
                subject = value;
                break;
            case 'grade':
                grade = value;
                break;
            case 'level':
                level = value;
                break;
            case 'sort':
                sort = value;
                break;
            case 'search':
                searchValue = value;
                break;
            default:
                break;
        }
    }

    $.ajax({
        url: '/Umbraco/Api/Materials/GetMaterials',
        method: 'GET',
        data: {
            subject: subject,
            grade: grade,
            level: level,
            sort: sort,
            page: page,
            searchText: searchValue
        },
        success: function (response) {
            // Оновлення контенту сторінки з отриманими даними
            updateContent(response);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

// Функція для оновлення контенту сторінки з отриманими даними
function updateContent(data) {
    // Очистити поточний вміст блоку матеріалів
    $('.matirials__block').empty();

    // Додати нові матеріали до блоку матеріалів
    data.materials.forEach(function (material) {
        var html = `
                    <div class="matirials__column">
                        <a class="matirials__item" href="${material.linkUrl}">
                            ${material.imageUrl ? `<div class="matirials__item-image"><img src="${material.imageUrl}" alt="Item image"></div>` : ''}
                            <h3 class="matirials__item-title">${material.title}</h3>
                            <div class="matirials__item-tags">
                                ${material.tags.map(tag => `<span class="matirials__item-tag">${tag}</span>`).join('')}
                            </div>
                            ${material.text ? `<p class="matirials__item-text">${material.text}</p>` : ''}
                        </a>
                    </div>
                `;
        $('.matirials__block').append(html);
    });

    // Оновити пагінацію
    updatePagination(data);
}

// Функція для оновлення пагінації
function updatePagination(data) {
    // Очистити поточний вміст пагінації
    $('.pagination__list').empty();

    // Додати пагінацію для кожної сторінки
    for (var i = 1; i <= data.totalPages; i++) {
        var activeClass = (i === data.currentPage) ? 'active' : '';
        var html = `<li class="pagination__list-item"><a class="pagination__list-link ${activeClass}" href="#">${i}</a></li>`;
        $('.pagination__list').append(html);
    }
}