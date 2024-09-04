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
    const languageElement = document.getElementById('language');
    const items = document.querySelectorAll('.column__item');

    const culture = getCountryNameFromLocale(select.getAttribute('data-culture'));
    if (culture) {
        const optionToSelect = select.querySelector(`option[value="${culture}"]`);
        if (optionToSelect) {
            optionToSelect.selected = true;
        }
    }

    function updateUrl(item) {
        const selectedValue = select.value;
        const url = new URL(item.href);
        const queryValue = item.getAttribute('data-query');
        return `${url.origin}${url.pathname}${selectedValue.toLowerCase()}/${queryValue.toLowerCase()}`;
    }

    items.forEach(item => {
        item.addEventListener('click', function (event) {
            event.preventDefault();
            const newUrl = updateUrl(this);
            window.location.href = newUrl;
        });
    });

    languageElement.addEventListener('change', handleLanguageChange);

    handleLanguageChange();
    async function handleLanguageChange() {
        const selectedValue = languageElement.value;
        const culture = languageElement.getAttribute('data-culture');

        const requests = Array.from(items).map(item => {
            return $.ajax({
                url: `/Umbraco/Api/Materials/IsMaterialPageHasChild`,
                type: 'GET',
                data: { language: selectedValue, pageName: item.innerHTML, culture: culture },
                success: function (result) {
                    if (!result) {
                        item.classList.add('disable');
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });       
    }
});

function getCountryNameFromLocale(locale) {
    const regionNames = new Intl.DisplayNames(['en'], { type: 'region' });

    const countryCode = locale.split('-')[1].toUpperCase();

    return regionNames.of(countryCode);
}