import { FormControl, FormGroup, Validators } from "@angular/forms";

// Regex
export const EmailRegex: string =
  "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
export const BRGovIdRegex: string =
  "(^\\d{3}.\\d{3}\\.\\d{3}\\-\\d{2}$)|(^\\d{2}\\.\\d{3}\\.\\d{3}\\/\\d{4}\\-\\d{2}$)";
export const BRPhoneRegex: string =
  "(\\([1-9][0-9]\\)?|[1-9][0-9])\\s?([9]{1})?([0-9]{4})-?([0-9]{4})$";
export const BRPostalCode: string = "^\\d{5}-?\\d{3}$";

// Password
export const PasswordRegex: string = "^(?=.*).{4,18}$";
export const PasswordRequirementMessagePT: string =
  "Sua senha deve ter no mínimo 10 caracteres, no máximo 18 e conter um ou mais caracteres alfanuméricos (0-9)";
export const PasswordControl: FormControl = new FormControl("", [
  Validators.required,
  Validators.pattern(PasswordRegex),
  Validators.minLength(10),
  Validators.maxLength(18)
]);
