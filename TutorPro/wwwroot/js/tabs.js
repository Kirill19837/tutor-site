document.addEventListener('DOMContentLoaded', function () {
    const autoMoveDelay = document.querySelector('.tabs').getAttribute('data-autoMoveDelay') * 1000;
    const tabs = document.querySelectorAll('.tab-button');
    const contents = document.querySelectorAll('.tab-content');
    let currentTabIndex = 0;
    let intervalId;

    function showTab(index) {
        tabs.forEach((t, i) => {
            if (i === index) {
                t.classList.add('active');
                contents[i].style.display = 'flex';
            } else {
                t.classList.remove('active');
                contents[i].style.display = 'none';
            }
        });
    }

    function rotateTab() {
        currentTabIndex = (currentTabIndex + 1) % tabs.length;
        showTab(currentTabIndex);
    }

    function startRotation() {
        if (window.innerWidth >= 768) {
            if (intervalId) clearInterval(intervalId);
            intervalId = setInterval(rotateTab, autoMoveDelay);
        }       
    }

    function stopRotation() {
        if (intervalId) clearInterval(intervalId);
    }

    tabs.forEach((tab, index) => {
        tab.addEventListener('click', () => {
            currentTabIndex = index;
            showTab(currentTabIndex);
            stopRotation();
            startRotation();
        });
    });

    startRotation();

    document.querySelector('.tabs').addEventListener('mouseenter', stopRotation);
    document.querySelector('.tabs').addEventListener('mouseleave', startRotation);
    document.querySelector('.tabs_content').addEventListener('mouseenter', stopRotation);
    document.querySelector('.tabs_content').addEventListener('mouseleave', startRotation);

    window.addEventListener('resize', () => {
        if (window.innerWidth < 768) {
            stopRotation(); 
        } else {
            startRotation();
        }
    });
});