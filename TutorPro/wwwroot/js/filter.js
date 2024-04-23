
$(document).ready(function () {
    sendRequest();
        $('.filter-form__select').on('change', function () {
            sendRequest();
        });

        document.querySelector('.filter-form__input').addEventListener('input', function () {
            var searchValue = this.value;
            sendRequest("search", searchValue); // Passing the new value of searchValue to the sendRequest function
        });

        $('.pagination__list').on('click', '.pagination__list-link', function (e) {
            e.preventDefault();
            var page = $(this).text(); // Get the page number
            sendRequest(null, null, page); // Send an AJAX request with the page number
        });

});


// Function for sending AJAX request
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

    // Checking if a new key and value have been passed
    if (key !== null && value !== null) {
        // Adding a new key and its value to the request parameters object
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
            // Updating the page content with the received data
            updateContent(response);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

// Function for updating the page content with the received data
function updateContent(data) {
    // Clear the current content of the materials block
    $('.matirials__block').empty();

    // Add new materials to the materials block
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