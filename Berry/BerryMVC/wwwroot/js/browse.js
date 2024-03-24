

const inputResult = document.getElementById("input-result");
const inputVendor = document.getElementById("input-vendor");
const inputArtifact = document.getElementById("input-artifact");
const inputVersion = document.getElementById("input-version");

const inputs = [inputVendor, inputArtifact, inputVersion]

function updateInputResult() {

    var nonnull = false;
    inputs.forEach(i => {
        if (i.value != null && i.value.trim() !== '') {
            nonnull = true;
        }
    });

    var concatenated = "";
    if (nonnull) {
        concatenated = "".concat(
            inputVendor.value || "*", 
            ":", 
            inputArtifact.value || "*", 
            "@", 
            inputVersion.value || "*"
            );
    }
    inputResult.value = concatenated;
}

inputs.forEach(i => {
    i.addEventListener('input', () => updateInputResult());
    i.addEventListener('propertychange', () => updateInputResult());
});