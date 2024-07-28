document.addEventListener('DOMContentLoaded', function () {
    const textElements = document.querySelectorAll('.services-header, .image-for-home-delivery, .image-for-consulting, .image-for-geniune-product, .header-for-home, .header-for-consulting, .header-for-geniune, .text-for-home, .text-for-genuine, .text-for-consulting-container, .paragraph-for-consulting, .paragraph-for-consulting-1');

    function addAnimationClass() {
        textElements.forEach(element => {
            element.classList.add('animate');
        });
    }

    function onScroll() {
        const elementPosition = textElements[0].getBoundingClientRect().top;

        if (elementPosition < window.innerHeight) {
            addAnimationClass();
            window.removeEventListener('scroll', onScroll);
        }
    }
    window.addEventListener('scroll', onScroll);
});