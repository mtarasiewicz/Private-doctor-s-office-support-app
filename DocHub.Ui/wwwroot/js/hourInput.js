document.addEventListener('DOMContentLoaded', function () {
    const timeInput = document.getElementById('timeInput');

    timeInput.addEventListener('input', function (event) {
        let inputValue = event.target.value.replace(/\D/g, ''); // Usunięcie wszystkich niecyfrowych znaków

        if (inputValue.length > 2) {
            // Dodanie dwukropka po dwóch pierwszych cyfrach
            inputValue = inputValue.slice(0, 2) + ':' + inputValue.slice(2);
        }

        // Ograniczenie długości do 5 znaków (np. "hh:mm")
        inputValue = inputValue.slice(0, 5);

        // Ustawienie sformatowanego tekstu w polu tekstowym
        timeInput.value = inputValue;
    });
});