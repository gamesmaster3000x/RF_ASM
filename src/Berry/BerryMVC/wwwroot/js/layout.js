const COLOUR_MODE_PREFERENCE_COOKIE = "color-mode-preference";

const toggles = document.getElementsByClassName("mode-toggle");

const themes = [
    {name: "light", active: '<i class="bi bi-brightness-high-fill"> </i>', inactive: '<i class="bi bi-brightness-high"> </i>'},
    {name: "dark", active:  '<i class="bi bi-moon-fill"> </i>', inactive: '<i class="bi bi-moon"> </i>'}, 
    //{name: "blue", active: '<i class="bi bi-search"></i>', inactive: '<i class="bi bi-moon"> </i>'}
];

function onclick_modeToggle() {
    var currentTheme = document.body.getAttribute("data-bs-theme");
    var index = themes.findIndex(t => t.name == currentTheme) ?? null;

    index = (index + 1 >= themes.length) ? 0 : index + 1;
    var newTheme =  themes[index];
    setTheme(newTheme);
}

function setTheme(newTheme) {
    document.body.setAttribute("data-bs-theme", newTheme.name);

    var span = '<span style="margin: 5px;">'

    for (var i = 0; i < themes.length; i++) {
        var theme = themes[i];
        if (theme.name == newTheme.name) 
            span = span.concat(theme.active);
        else 
            span = span.concat(theme.inactive);
    }

    span = span.concat('</span>')

    for (var i = 0; i < toggles.length; i++) {
        toggles[i].innerHTML = span;
    }

    console.log("Changing theme from to", newTheme);
}

function getNamedTheme(name) {
    return themes.filter(t => t.name == name)[0] || "light";
}

function e_onDOMLoaded() {
    var theme;
    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        theme = getNamedTheme("dark")
    } else {
        theme = getNamedTheme("light");
    }
    setTheme(theme);
}