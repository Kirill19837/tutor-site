var fixedLightTheme = false;

if (header.classList.contains('light-mode')) {
    fixedLightTheme = true;
}

function toggleDropdown(element) {
    const header = document.querySelector('.header');
    element.classList.toggle('dropdown');     
    const dropdownContent = document.querySelector('.dropdown-content');

    if (dropdownContent) {
        if (window.innerWidth > 992) {                      
            if (!fixedLightTheme) {
                header.classList.toggle('light-mode');
            }
        }

        dropdownContent.classList.toggle('show');
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const select = document.querySelector('.column__select');
    const culture = getCountryNameFromLocale(select.getAttribute('data-culture'));

    if (culture) {
        const optionToSelect = select.querySelector(`option[value="${culture}"]`);
        if (optionToSelect) {
            optionToSelect.selected = true;
        }
    }

    const items = document.querySelectorAll('.column__item');
    function updateUrl(item) {
        const selectedValue = select.value;

        const url = new URL(item.href);

        const params = new URLSearchParams(url.search);

        params.set('Language', selectedValue);

        const queryValue = item.getAttribute('data-query');
        params.set('Filter', queryValue);

        return `${url.origin}${url.pathname}?${params.toString()}`;
    }

    items.forEach(item => {
        item.addEventListener('click', function (event) {
            event.preventDefault();

            const newUrl = updateUrl(this);

            window.location.href = newUrl;
        });
    });
});

function getCountryNameFromLocale(locale) {
    const regionNames = new Intl.DisplayNames(['en'], { type: 'region' });

    const countryCode = locale.split('-')[1].toUpperCase();

    return regionNames.of(countryCode);
}