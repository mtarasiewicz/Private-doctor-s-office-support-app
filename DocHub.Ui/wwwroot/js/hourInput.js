document.addEventListener('DOMContentLoaded', function () {
    const timeInput = document.getElementById('timeInput');

    timeInput.addEventListener('input', function (event) {
        let inputValue = event.target.value.replace(/\D/g, '');
        if (inputValue.length > 2) {
            inputValue = inputValue.slice(0, 2) + ':' + inputValue.slice(2);
        }
        inputValue = inputValue.slice(0, 5);
        timeInput.value = inputValue;
    });
});