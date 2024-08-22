document.addEventListener("DOMContentLoaded", function () {
    var theme = localStorage.getItem('theme') || 'light';
    document.getElementById('darkTheme').disabled = theme !== 'dark';
    document.getElementById('lightTheme').disabled = theme !== 'light';

    document.getElementById('themeToggle').addEventListener('click', function () {
        var newTheme = theme === 'dark' ? 'light' : 'dark';
        document.getElementById('darkTheme').disabled = newTheme !== 'dark';
        document.getElementById('lightTheme').disabled = newTheme !== 'light';
        localStorage.setItem('theme', newTheme);
        theme = newTheme;
    });
});
