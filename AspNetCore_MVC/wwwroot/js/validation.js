const textValidator = (element, minLength = 2) => {
    if (element.value.length >= minLength)
        formErrorHandler(element, true)

    formErrorHandler(element, false)
};

const compareValidator = (element, compareTo) => {
    return element.value === compareTo;
};


const formErrorHandler = (element, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);

    if (validationResult) {
        element.classList.remove('input-validation-error');
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = '';
    }
    else {
        element.classList.add('input-validation-error');
        spanElement.classList.add('field-validation-error');
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = element.dataset.valRequired
    }
};

const checkboxValidator = (element) => {
    if (element.checked) {
        formErrorHandler(element, true);
    } else {
        formErrorHandler(element, false);
    }
};

const emailValidator = (element) => {
    const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    formErrorHandler(element, regEx.test(element.value))
}

const passwordValidator = (element) => {
    if (element.dataset.valEqualToOther !== undefined) {
        const otherElementValue = document.getElementsByName(element.dataset.valEqualToOther.replace('*', '') + 'Form')[0].value;
        formErrorHandler(element, compareValidator(element, otherElementValue));
    } else {
        const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$/;
        formErrorHandler(element, regEx.test(element.value)); 
    }
};

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.target)
            })
        }
        else {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {

                    case 'text':
                        textValidator(e.target)
                        break;

                    case 'email':
                        emailValidator(e.target)
                        break;

                    case 'password':
                        passwordValidator(e.target)
                }
            })
        }

    }
})