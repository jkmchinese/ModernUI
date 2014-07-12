using System;
using System.Diagnostics.CodeAnalysis;

namespace ModernUI.Windows.Controls.BBCode
{
    /// <summary>
    /// Represents a character buffer.
    /// </summary>
    internal class CharBuffer
    {
        private readonly string m_value;
        private int m_position;
        private int m_mark;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CharBuffer"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public CharBuffer(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.m_value = value;
        }

        /// <summary>
        /// Performs a look-ahead.
        /// </summary>
        /// <param name="count">The number of character to look ahead.</param>
        /// <returns></returns>
        public char LA(int count)
        {
            int index = this.m_position + count - 1;
            if (index < this.m_value.Length)
            {
                return this.m_value[index];
            }

            return char.MaxValue;
        }

        /// <summary>
        /// Marks the current position.
        /// </summary>
        public void Mark()
        {
            this.m_mark = this.m_position;
        }

        /// <summary>
        /// Gets the mark.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public string GetMark()
        {
            if (this.m_mark < this.m_position)
            {
                return this.m_value.Substring(this.m_mark, this.m_position - this.m_mark);
            }
            return string.Empty;
        }

        /// <summary>
        /// Consumes the next character.
        /// </summary>
        public void Consume()
        {
            this.m_position++;
        }
    }
}
