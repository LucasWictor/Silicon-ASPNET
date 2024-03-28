document.querySelectorAll('form [data-val="true"]').forEach(input => {
    input.addEventListener('input', function(e) {
        if (e.target.type === 'email') {
            validateEmail(e.target);
        } else if (e.target.dataset.valEqualToOther) {
            validateConfirmPassword(e.target); // This will now validate live
        } else if (e.target.type === 'password') {
            validatePassword(e.target); // Add this line to validate passwords live
            // If this is the original password, also trigger validation on the confirm password field if it's been used
            const confirmPasswordField = document.querySelector(`[data-val-equalto-other="${e.target.name}"]`);
            if (confirmPasswordField) {
                validateConfirmPassword(confirmPasswordField);
            }
        } else {
            validateText(e.target, e.target.dataset.valMinLength ? parseInt(e.target.dataset.valMinLength, 10) : 2);
        }
    });
});

function validateText(input, minLength) {
    const isValid = input.value.length >= minLength;
    formErrorHandler(input, isValid, 'The input is too short.');
}

function validateEmail(input) {
    const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    const isValid = regEx.test(input.value);
    formErrorHandler(input, isValid, 'Please enter a valid email address.');
}

function validateConfirmPassword(input) {
    const originalPassword = document.querySelector(`[name="${input.dataset.valEqualToOther}"]`).value;
    const isValid = input.value === originalPassword && input.value !== '';
    formErrorHandler(input, isValid, 'Passwords do not match.');
}

function validatePassword(input) {
    // Password must contain at least one number, one uppercase and lowercase letter, and one special character
    const regEx = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{8,}$/;
    const isValid = regEx.test(input.value);
    formErrorHandler(input, isValid, 'Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.');
}

function formErrorHandler(input, isValid, errorMessage) {
    let feedbackElement = document.querySelector(`[data-valmsg-for="${input.name}"]`);
    if (isValid) {
        input.classList.remove('input-validation-error');
        if (feedbackElement) {
            feedbackElement.classList.remove('field-validation-error');
            feedbackElement.textContent = '';
        }
    } else {
        input.classList.add('input-validation-error');
        if (feedbackElement) {
            feedbackElement.classList.add('field-validation-error');
            feedbackElement.textContent = errorMessage;
        }
    }
}
