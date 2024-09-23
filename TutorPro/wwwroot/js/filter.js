$(document).ready(function () {
    // load chosen page
    var currentPage = getParameterByName('page');
    if (currentPage) {
        handlePageChange(currentPage);
    } else {
        handlePageChange(1);
    }

    initFilters();

    $('.filter-form__select').on('change', function () {
        sendRequest();
    });

    let searchTimeout;
    let articleUrlElement = document.querySelector('[data-searchDelay]');
    let delay = articleUrlElement.getAttribute('data-searchDelay');
       
    document.querySelector('.filter-form__input').addEventListener('input', function () {
        clearTimeout(searchTimeout);
        let searchValue = this.value;
        searchTimeout = setTimeout(function () {
            sendRequest("search", searchValue); // Passing the new value of searchValue to the sendRequest function
        }, delay); // delay time
    });

    $('.pagination__list').on('click', '.pagination__list-link', function (e) {
        e.preventDefault();
        var page = $(this).text(); // get page number
        handlePageChange(page);
    });
});


// Function for sending AJAX request
function sendRequest(key = null, value = null, page = 1) {
    var sort = $('#sort').siblings('.new-select__list').find('.new-select__item.selected').attr('data-value');

    var searchValue = document.getElementsByClassName('filter-form__input')[0].value

    // Checking if a new key and value have been passed
    if (key !== null && value !== null) {
        // Adding a new key and its value to the request parameters object
        switch (key) {          
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

    var pagination = document.querySelector('.matirials__pagination');
    var pageSize = pagination.getAttribute('data-pageSize');
    const filterElement = document.getElementById('materials_filter');
    const language = filterElement.getAttribute('data-language');
    const pageId = filterElement.getAttribute('data-pageId');

    $.ajax({
        url: '/Umbraco/Api/Materials/GetMaterials',
        method: 'POST',
        contentType: 'application/json; charset=utf-8',
        headers: {
            'Accept': 'application/json'
        },
        data: JSON.stringify({
            subject: subjects,
            language: language,
            categoryItems: getCategories(),
            sort: sort,
            page: page,
            pageSize: pageSize,
            pageId: pageId,
            searchText: searchValue,
        }),
        success: function (response) {
            // Updating the page content with the received data
            updateContent(response);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

function getSelectedItemsByCategory(category) {
    const selector = `.selected[data-categorie="${category}"]`;
    return document.querySelectorAll(selector);
}
function getSelectedKeysByCategory(category) {
    const selectedItems = getSelectedItemsByCategory(category);
    const keys = Array.from(selectedItems).map(item => item.getAttribute('data-key'));
    return keys;
}

// Function for updating the page content with the received data
function updateContent(data) {
    // Clear the current content of the materials block
    $('.matirials__block').empty();

    var reviewsText = $('.matirials__block').attr('data-reviews'); 
    var culture = $('.matirials__block').attr('data-culture'); 

    // Add new materials to the materials block
    if (data.materials && data.materials.length > 0) {
        // Add new materials to the materials block
        data.materials.forEach(function (material) {
            const type = Math.floor(Math.random() * 4) + 1;
            const date = new Date(material.updatedDate);
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            const formattedDate = date.toLocaleDateString(`${culture}`, options);
            var html = `
                <div class="matirials__column">
                    <a class="matirials__item" href="${material.url}">
                    <div class="materials__item_content">
                        ${material.imageUrl ? `<div class="matirials__item-image"><img loading="lazy" src="${material.imageUrl}" alt="Item image"></div>` : `<div class="matirials__item-image"><img loading="lazy" src="/images/matirials/Type=${type}.png" alt="Item image"></div>`}
                        <h3 class="matirials__item-title">${material.title}</h3>
                        <div class="matirials__item-tags">
                            ${material.tags.map(tag => `<span class="matirials__item-tag">${tag.replace('#', '')}</span>`).join('')}
                        </div>
                        ${material.text ? `<p class="matirials__item-text">${material.text}</p>` : ''}
                    </div>
                        <div class="materials__item_info">
                            <div><img src="/images/icons/users-round-16px.svg"/> ${material.viewsNumber} <span>${reviewsText}</span></div>
                            <div><img src="/images/icons/calendar-16px.svg"/> ${formattedDate}</div>
                        </div>
                    </a>
                </div>
            `;
            $('.matirials__block').append(html);
        });
    } else {
        // Get the not found message from data-notFoundMessage attribute
        var notFoundMessage = $('.matirials__block').attr('data-notFoundMessage');
        // Display not found message
        $('.matirials__block').html(`<h1 style="width: 100%; text-align: center;">${notFoundMessage}</h1>`);

    }

    // Update pagination
    updatePagination(data);
}

// Function for updating pagination
function updatePagination(data) {
    // Clear the current content of pagination
    $('.pagination__list').empty();

    // Add pagination for each page
    for (var i = 1; i <= data.totalPages; i++) {
        var activeClass = (i === data.currentPage) ? 'active' : '';
        var html = `<li class="pagination__list-item"><a class="pagination__list-link ${activeClass}" href="#">${i}</a></li>`;
        $('.pagination__list').append(html);
    }
}

function getParameterByName(name) {
    var url = new URL(window.location.href);
    return url.searchParams.get(name);
}

function handlePageChange(page) {
    sendRequest(null, null, page);
    history.pushState(null, null, '?page=' + page);
}

function initFilters() {
    const items = document.querySelectorAll('.matirials__category-item');

    items.forEach(item => {
        item.addEventListener('click', () => {
            item.classList.toggle('selected');
            sendRequest()
        });
    });
}

function getCategories() {
    const groupedItems = {};

    $('.matirials__category-item.selected').each(function () {
        const categorie = $(this).data('categorie');
        const key = $(this).data('key');

        if (!groupedItems[categorie]) {
            groupedItems[categorie] = [];
        }

        groupedItems[categorie].push(key);
    });

    const resultArray = Object.keys(groupedItems).map(categorie => ({
        category: categorie,
        items: groupedItems[categorie]
    }));

    return resultArray;
}