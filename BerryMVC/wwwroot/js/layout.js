

const toggles = document.querySelectorAll(".mode-toggle");
const themes = ["light", "dark", "blue"];

function onclick_modeToggle() {
    var currentTheme = document.body.getAttribute("data-bs-theme");
    var index = themes.indexOf(currentTheme);

    index = (index + 1 >= themes.length) ? 0 : index + 1;
    var newTheme =  themes[index];
    document.body.setAttribute("data-bs-theme", newTheme);

    console.log("Changing theme from", currentTheme, "to", newTheme);
}
