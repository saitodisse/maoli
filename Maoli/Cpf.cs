﻿namespace Maoli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a valid CPF number
    /// </summary>
    public class Cpf
    {
        private string rawValue;

        private string parsedValue;

        /// <summary>
        /// Gets the puntuaction setting
        /// </summary>
        public CpfPunctuation Punctuation { get; private set; }

        /// <summary>
        /// Initializes a new instance of Cpf
        /// </summary>
        /// <param name="value">a valid CPF string</param>
        public Cpf(string value)
            : this(value, CpfPunctuation.Loose)
        {
        }

        /// <summary>
        /// Initializes a new instance of Cpf
        /// </summary>
        /// <param name="value">a valid CPF string</param>
        /// <param name="puntuaction">the puntuaction setting configurating 
        /// how validation must be handled</param>
        public Cpf(string value, CpfPunctuation puntuaction)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O CPF não pode ser nulo ou branco");
            }

            if (!CpfHelper.Validate(value, puntuaction))
            {
                throw new ArgumentException("O CPF não é válido");
            }

            this.rawValue = value;
            this.parsedValue = CpfHelper.Sanitize(value);

            this.Punctuation = puntuaction;
        }

        public static Cpf Parse(string value)
        {
            return Cpf.Parse(value, CpfPunctuation.Loose);
        }

        public static Cpf Parse(string value, CpfPunctuation puntuaction)
        {
            return new Cpf(value, puntuaction);
        }

        public static bool TryParse(string value, out Cpf cpf)
        {
            return Cpf.TryParse(value, out cpf, CpfPunctuation.Loose);
        }

        public static bool TryParse(string value, out Cpf cpf, CpfPunctuation punctuation)
        {
            var parsed = false;

            try
            {
                cpf = new Cpf(value, punctuation);

                parsed = true;
            }
            catch (ArgumentException)
            {
                cpf = null;

                parsed = false;
            }

            return parsed;
        }

        /// <summary>
        /// Checks if a string value is a valid CPF representation
        /// </summary>
        /// <param name="value">a CPF string to be checked</param>
        /// <returns>true if CPF string is valid; false otherwise</returns>
        public static bool IsValid(string value)
        {
            return CpfHelper.Validate(value, CpfPunctuation.Loose);
        }

        /// <summary>
        /// Checks if a string value is a valid CPF representation
        /// </summary>
        /// <param name="value">a CPF string to be checked</param>
        /// <param name="puntuaction">the puntuaction setting configurating 
        /// how validation must be handled</param>
        /// <returns>true if CPF string is valid; otherwise, false</returns>
        public static bool IsValid(string value, CpfPunctuation puntuaction)
        {
            return CpfHelper.Validate(value, puntuaction);
        }

        /// <summary>
        /// Completes a partial CPF string by appending a valid checksum trailing
        /// </summary>
        /// <param name="value">a partial CPF string with or without puntuaction</param>
        /// <returns>a CPF string with a valid checksum trailing</returns>
        public static string Complete(string value)
        {
            return CpfHelper.Complete(value);
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Cpf"/> object, have the same value
        /// </summary>
        /// <param name="obj">The Cpf to compare to this instance</param>
        /// <returns>if the value of the value parameter is the same as this instance; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Cpf);
        }

        /// <summary>
        /// Determines whether this instance and another specified <see cref="Cpf"/> object have the same value
        /// </summary>
        /// <param name="cpf">The Cpf to compare to this instance</param>
        /// <returns>if the value of the value parameter is the same as this instance; otherwise, false</returns>
        public bool Equals(Cpf cpf)
        {
            if (cpf == null)
            {
                return false;
            }

            return this.parsedValue == cpf.parsedValue;
        }

        /// <summary>
        /// Returns the hash code for this Cpf
        /// </summary>
        /// <returns>A 32-bit signed integer hash code</returns>
        public override int GetHashCode()
        {
            int hash = 17;

            unchecked
            {
                hash = hash * 31 + (string.IsNullOrWhiteSpace(this.parsedValue) ? 0 : this.parsedValue.GetHashCode());
            }

            return hash;
        }

        /// <summary>
        /// Converts the value of this instance to a <seealso cref="string"/>String.
        /// </summary>
        /// <returns>The cpf as string</returns>
        public override string ToString()
        {
            return this.parsedValue;
        }
    }
}
