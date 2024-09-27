// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// JavaScript para pagina de detalhes de cada casa
document.querySelectorAll('.thumbnail-container img').forEach((thumbnail) => {
    thumbnail.addEventListener('click', function () {
        document.querySelectorAll('.thumbnail-container img').forEach((img) => {
            img.classList.remove('active');
        });
        this.classList.add('active');
    });
});
