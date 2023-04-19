module.exports = {
    purge: {
        enabled: true,
        content: [
            './Pages/**/*.cshtml',
            './Views/**/*.chstml'
        ]
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {},

        colors: {
            transparent: 'transparent',
            current: 'currentColor',
            'magenta': '#92374d',
            'mint': '#cbf3f0',
            'white': '#e9e8ed',
            'tangerine': '#f68f5e',
            'bittersweet': '#f76d5f',
            'majorelle': '#574AE2',
            'royal': '#222A68',
            'hunter': '#4B644A',
            'ash': '#c7d6d5',
            'quartz': '#9C92A3',
        }
    },
    variants: {
        extend: {},
    },
    plugins: [],
}