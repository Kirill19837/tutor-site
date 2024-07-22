$(document).ready(function () {
    sendRequest();

    let searchTimeout;
    let articleUrlElement = document.querySelector('[data-searchDelay]');
    let delay = articleUrlElement != null ? articleUrlElement.getAttribute('data-searchDelay') : 0;
    $('.filter-form__input').on('input', function () {
        clearTimeout(searchTimeout)
        var searchText = $(this).val();
        searchTimeout = setTimeout(function () {
            sendRequest(searchText); // Passing the new value of searchText to the sendRequest function
        }, delay); // delay
        
    });

    $('.pagination__list').on('click', '.pagination__list-link', function (e) {
        e.preventDefault();
        var page = $(this).text(); // Getting the page number
        sendRequest(null, page);
    });
});

// Function for sending an AJAX request
function sendRequest(searchText = null, page = 1) {
    
    var searchValue = $('.filter-form__input').val();

    // Checking if a new value has been passed
    if (searchText != null) {
        searchValue = searchText
    }

    var block = document.querySelector('.blog__block');
    var pageSize = block.getAttribute('data-pageSize');
    var culture = block.getAttribute('data-culture');
    var category = block.getAttribute('data-category');
  
    $.ajax({
        url: '/Umbraco/Api/Blog/GetBlogs',
        method: 'GET',
        data: {
            searchText: searchValue,
            culture: culture,
            page: page,
            pageSize: pageSize,   
            category: category,
        },
        success: function (response) {
            updateContent(response);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

// Function for updating the page content with the received data
function updateContent(data) {
    var block = document.querySelector('.blog__block');
    var culture = block.getAttribute('data-culture');
    var border = block.getAttribute('data-border');

    $('.blog__block').empty();  

    // Adding new blocks with the received data
    if (data.blogs && data.blogs.length > 0) {
        data.blogs.forEach(function (blog) {
            const date = new Date(blog.dateTime);
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            const formattedDate = date.toLocaleDateString(`${culture}`, options);
            var additionalClasses = border === 'True' ? ' blog__item--with-border' : '';
            var dateClass = border === 'True' ? 'blog__item-date' : 'blog__item-date--relative';
            var additionalTitleClasses = border === 'True' ? ' blog__item-title--margin' : '';
            var blogItem = `
                <a class="blog__item${additionalClasses}" href="${blog.url}">
                    ${blog.imageUrl ? `
                        <div class="blog__item-image">
                            <img loading="lazy" src="${blog.imageUrl}" alt="Article image">
                        </div>
                    ` : `
                        <div class="blog__item-image">
                            <img>
                        </div>
                    `}
                    ${blog.title ? `<p class="blog__item-title${additionalTitleClasses}">${blog.title}</p>` : ''}
                    <span class="${dateClass}">${formattedDate}</span>
                </a>
            `;
            $('.blog__block').append(blogItem);
        });
    } else {
        // Displaying a message indicating no search results
        var block = document.querySelector('.blog__block');
        var message = block.getAttribute('data-notFoundMessage');
        $('.blog__block').html(`<h1 style="width: 100%; text-align: center;">${message}</h1>`);
    }

    updatePagination(data.totalPages, data.currentPage);
}

function updatePagination(totalPages, currentPage) {
    $('.pagination__list').empty();

    for (var i = 1; i <= totalPages; i++) {
        var activeClass = (i === currentPage) ? 'active' : '';
        var html = `<li class="pagination__list-item"><a class="pagination__list-link ${activeClass}" href="#">${i}</a></li>`;
        $('.pagination__list').append(html);
    }
}
