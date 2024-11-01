// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


<script>
    document.addEventListener('DOMContentLoaded', () => {
        const imagensContainer = document.querySelector('.imagens');
    const imagens = document.querySelectorAll('.imagem-carrossel');
    const prevButton = document.querySelector('.prev');
    const nextButton = document.querySelector('.next');

    let currentIndex = 0;

    function updateCarousel() {
            const offset = -currentIndex * 100; // Calcular o deslocamento
    imagensContainer.style.transform = `translateX(${offset}%)`;
        }

        prevButton.addEventListener('click', () => {
        currentIndex = (currentIndex > 0) ? currentIndex - 1 : imagens.length - 1;
    updateCarousel();
        });

        nextButton.addEventListener('click', () => {
        currentIndex = (currentIndex < imagens.length - 1) ? currentIndex + 1 : 0;
    updateCarousel();
        });
    });
</script>
