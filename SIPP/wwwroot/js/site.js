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

    document.addEventListener("DOMContentLoaded", function () {
        // Limitar a data para segunda a sábado
        var dateInput = document.getElementById("DataAge");
        dateInput.addEventListener("input", function () {
            var dateValue = new Date(dateInput.value);
            var dayOfWeek = dateValue.getUTCDay();

            // Verificar se é domingo (0)
            if (dayOfWeek === 0) {
                alert("Não é permitido agendar no domingo. Por favor, escolha uma data de segunda a sábado.");
                dateInput.setCustomValidity("Data inválida");
            } else {
                dateInput.setCustomValidity(""); // Limpa a validação
            }
        });

        // Limitar o horário para horários específicos (por exemplo, entre 09:00 e 18:00)
        var timeInput = document.getElementById("HoraAge");
        timeInput.addEventListener("input", function () {
            var timeValue = timeInput.value;
            var timeParts = timeValue.split(":");
            var hour = parseInt(timeParts[0], 10);

            // Se o horário for antes das 09:00 ou depois das 18:00, exibe um alerta
            if (hour < 9 || hour >= 18) {
                alert("O horário permitido é entre 09:00 e 18:00.");
                timeInput.setCustomValidity("Horário inválido");
            } else {
                timeInput.setCustomValidity(""); // Limpa a validação
            }
        });
    });


    
</script>
