let slider = document.querySelector('.slider');

$('.slider').slick({
    speed: 500,
    autoplay: false,
    slidesToShow: 2,
    infinite: true,
    arrows: true,
    draggable: true,
    swipe: true,

    nextArrow: '<button class="slider_arrow slider_arrow--next" aria-label="Next"><svg width="14" height="23" viewBox="0 0 14 23" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M2.39844 21.5015L12.3984 11.5015L2.39844 1.50146" stroke="#C95500" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"/></svg></button>',

    prevArrow: `
            <button class="slider_arrow slider_arrow--prev" aria-label="Previous">
                <svg width="14" height="23" viewBox="0 0 14 23" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M11.5996 1.5L1.59961 11.5L11.5996 21.5" stroke="#C95500" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </button>`,
    responsive: [
        {
            breakpoint: 720, 
            settings: {
                slidesToShow: 1
            }
        }
    ]
});

function initializeStars(card) {
    const starsContainer = card.querySelector('.stars-container');
    if (starsContainer.children.length > 0) return;

    const rating = parseInt(card.dataset.rating) || 0;
    for (let i = 0; i < 5; i++) {
        starsContainer.innerHTML += `
                <svg width="16" height="16" viewBox="0 0 14 13" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="${i < rating ? 'M6.52447 0.463524C6.67415 0.00286841 7.32585 0.00286996 7.47553 0.463525L8.68386 4.18237C8.75079 4.38838 8.94277 4.52786 9.15938 4.52786H13.0696C13.554 4.52786 13.7554 5.14767 13.3635 5.43237L10.2001 7.73075C10.0248 7.85807 9.95149 8.08375 10.0184 8.28976L11.2268 12.0086C11.3764 12.4693 10.8492 12.8523 10.4573 12.5676L7.29389 10.2693C7.11865 10.1419 6.88135 10.1419 6.70611 10.2693L3.54267 12.5676C3.15081 12.8523 2.62357 12.4693 2.77325 12.0086L3.98157 8.28976C4.04851 8.08375 3.97518 7.85807 3.79994 7.73075L0.636495 5.43237C0.244639 5.14767 0.446028 4.52786 0.93039 4.52786H4.84062C5.05723 4.52786 5.24921 4.38838 5.31614 4.18237L6.52447 0.463524Z' : 'M6.52447 0.463524C6.67415 0.00286841 7.32585 0.00286996 7.47553 0.463525L8.68386 4.18237C8.75079 4.38838 8.94277 4.52786 9.15938 4.52786H13.0696C13.554 4.52786 13.7554 5.14767 13.3635 5.43237L10.2001 7.73075C10.0248 7.85807 9.95149 8.08375 10.0184 8.28976L11.2268 12.0086C11.3764 12.4693 10.8492 12.8523 10.4573 12.5676L7.29389 10.2693C7.11865 10.1419 6.88135 10.1419 6.70611 10.2693L3.54267 12.5676C3.15081 12.8523 2.62357 12.4693 2.77325 12.0086L3.98157 8.28976C4.04851 8.08375 3.97518 7.85807 3.79994 7.73075L0.636495 5.43237C0.244639 5.14767 0.446028 4.52786 0.93039 4.52786H4.84062C5.05723 4.52786 5.24921 4.38838 5.31614 4.18237L6.52447 0.463524Z'}" fill="${i < rating ? '#FF6B00' : '#FFC9A2'}"/>
                </svg>
            `;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const cards = document.querySelectorAll('.testimonial-card');
    cards.forEach(card => {
        initializeStars(card);
    });
});


