document.addEventListener("DOMContentLoaded", function () {
    const developedByPranab = document.querySelector(".developed-by-pranab");
    const copyrightGearventuresNepal = document.querySelector(".copyright-gearventures-nepal");
    const footerText = document.querySelector(".footer-text");

    function isInViewport(element) {
      const rect = element.getBoundingClientRect();
      return rect.top < window.innerHeight;
    }

    
    function animateOnScroll() {
      if (isInViewport(footerText)) {
        developedByPranab.style.transform = "translateX(0)";
        copyrightGearventuresNepal.style.transform = "translateX(0)";
        window.removeEventListener("scroll", animateOnScroll);
      }
    }

    window.addEventListener("scroll", animateOnScroll);
  });