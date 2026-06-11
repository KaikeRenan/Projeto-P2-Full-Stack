import type { ReactNode } from "react";

interface FieldWrapperProps {
  label: string;
  htmlFor: string;
  error?: string;
  hint?: string;
  children: ReactNode;
}

function FieldWrapper({ label, htmlFor, error, hint, children }: FieldWrapperProps) {
  return (
    <div>
      <label htmlFor={htmlFor} className="field-label">{label}</label>
      {children}
      {error && <p className="field-error">{error}</p>}
      {!error && hint && <p className="field-hint">{hint}</p>}
    </div>
  );
}

interface TextFieldProps extends FieldWrapperProps {
  type?: "text" | "email" | "date" | "datetime-local" | "number";
  value: string | number;
  onChange: (value: string) => void;
  placeholder?: string;
  required?: boolean;
  list?: string;
  min?: number;
  max?: number;
}

export function TextField({
  label, htmlFor, error, hint, type = "text", value, onChange, placeholder, required, list, min, max,
}: TextFieldProps) {
  return (
    <FieldWrapper label={label} htmlFor={htmlFor} error={error} hint={hint}>
      <input
        id={htmlFor}
        type={type}
        className="field-input"
        value={value}
        placeholder={placeholder}
        required={required}
        list={list}
        min={min}
        max={max}
        onChange={(e) => onChange(e.target.value)}
      />
    </FieldWrapper>
  );
}

interface SelectFieldProps extends FieldWrapperProps {
  value: string;
  onChange: (value: string) => void;
  options: { value: string; label: string }[];
  placeholder?: string;
  required?: boolean;
}

export function SelectField({ label, htmlFor, error, hint, value, onChange, options, placeholder, required }: SelectFieldProps) {
  return (
    <FieldWrapper label={label} htmlFor={htmlFor} error={error} hint={hint}>
      <select id={htmlFor} className="field-input" value={value} required={required} onChange={(e) => onChange(e.target.value)}>
        {placeholder && <option value="">{placeholder}</option>}
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>{opt.label}</option>
        ))}
      </select>
    </FieldWrapper>
  );
}

interface CheckboxFieldProps {
  label: string;
  htmlFor: string;
  checked: boolean;
  onChange: (checked: boolean) => void;
  hint?: string;
}

export function CheckboxField({ label, htmlFor, checked, onChange, hint }: CheckboxFieldProps) {
  return (
    <div className="flex items-start gap-2.5 py-1">
      <input
        id={htmlFor}
        type="checkbox"
        checked={checked}
        onChange={(e) => onChange(e.target.checked)}
        className="mt-0.5 w-4 h-4 rounded border-line text-primary focus:ring-primary/30"
      />
      <div>
        <label htmlFor={htmlFor} className="text-sm font-medium text-ink cursor-pointer">{label}</label>
        {hint && <p className="text-xs text-muted">{hint}</p>}
      </div>
    </div>
  );
}

interface TextareaFieldProps extends FieldWrapperProps {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  rows?: number;
}

export function TextareaField({ label, htmlFor, error, hint, value, onChange, placeholder, rows = 3 }: TextareaFieldProps) {
  return (
    <FieldWrapper label={label} htmlFor={htmlFor} error={error} hint={hint}>
      <textarea
        id={htmlFor}
        className="field-input resize-none"
        rows={rows}
        value={value}
        placeholder={placeholder}
        onChange={(e) => onChange(e.target.value)}
      />
    </FieldWrapper>
  );
}
