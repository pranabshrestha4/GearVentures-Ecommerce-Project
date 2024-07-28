const ecommerceappbodysection = document.querySelector('.e-commerce-app-body-section');

function addAnimationClass() {
    ecommerceappbodysection.classList.add('animate');
}

function onScroll() {
    const elementPosition = ecommerceappbodysection.getBoundingClientRect().top;

    if (elementPosition < window.innerHeight) {
        addAnimationClass();
        window.removeEventListener('scroll', onScroll); 
    }
}

window.addEventListener('scroll', onScroll);