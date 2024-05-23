$(document).ready(function () {
    sendRequest();

    $('.filter-form__input').on('input', function () {
        var searchText = $(this).val();
        sendRequest(searchText); // Passing the new value of searchText to the sendRequest function
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
  
    $.ajax({
        url: '/Umbraco/Api/Blog/GetBlogs',
        method: 'GET',
        data: {
            searchText: searchValue,
            culture: culture,
            page: page,
            pageSize: pageSize,           
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

    $('.blog__block').empty();  

    // Adding new blocks with the received data
    if (data.blogs && data.blogs.length > 0) {
        data.blogs.forEach(function (blog) {
            const date = new Date(blog.dateTime);
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            const formattedDate = date.toLocaleDateString(`${culture}`, options);
            var blogItem = `
                <a class="blog__item" href="${blog.url}">
                    ${blog.imageUrl ? `
                        <div class="blog__item-image">
                            <img src="${blog.imageUrl}" alt="Article image">
                        </div>
                    ` : ''}
                    ${blog.title ? `<h3 class="blog__item-title medium-title">${blog.title}</h3>` : ''}
                    <span class="blog__item-date">${formattedDate}</span>
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
