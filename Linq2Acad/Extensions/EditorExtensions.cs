using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for the AutoCAD Editor.
  /// </summary>
  public static class EditorExtensions
  {
    /// <summary>
    /// Displays a message on the AutoCAD text screen.
    /// </summary>
    /// <param name="editor">The editor instance.</param>
    /// <param name="formatString">A format string to display.</param>
    /// <param name="args">Arguments to the format string.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>formatString</i> is null.</exception>
    public static void WriteLine(this Editor editor, string formatString, params object[] args)
    {
      if (formatString == null) throw Error.ArgumentNull("formatString");
      editor.WriteMessage(string.Format("\n" + formatString, args));
    }

    /// <summary>
    /// Gets user input for a string.
    /// </summary>
    /// <remarks>After the user enters an input, the entered value is evaluated by the validation function. If the function evaluates to true, the Prompt result is returned.
    /// If the function evaluated to false, the user is again asked for the input.</remarks>
    /// <param name="editor">The editor instance.</param>
    /// <param name="message">Input message to be displayed to the user during the prompt.</param>
    /// <param name="validate">A function that validates the user input. If it evaluates to true, the PromptResult is returned. Else, the input message is repeatedly displayed.</param>
    /// <returns>Returns the PromptResult.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>message</i> or <i>validate</i> is null.</exception>
    public static PromptResult GetString(this Editor editor, string message, Func<string, bool> validate)
    {
      if (message == null) throw Error.ArgumentNull("message");
      if (validate == null) throw Error.ArgumentNull("validate");
      return GetStringInternal(editor, message, validate, null);
    }

    /// <summary>
    /// Gets user input for a string.
    /// </summary>
    /// <remarks>After the user enters an input, the entered value is evaluated by the validation function. If the function evaluates to true, the Prompt result is returned.
    /// If the function evaluated to false, the user is again asked for the input.</remarks>
    /// <param name="editor">The editor instance.</param>
    /// <param name="message">Input message to be displayed to the user during the prompt.</param>
    /// <param name="validate">A function that validates the user input. If it evaluates to true, the PromptResult is returned. Else, the input message is repeatedly displayed.</param>
    /// <param name="errorMessage">An error message that is displayed, if the validation function evaluates to false.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>message</i>, <i>validate</i> or <i>errorMessage</i> is null.</exception>
    /// <returns>Returns the PromptResult.</returns>
    public static PromptResult GetString(this Editor editor, string message, Func<string, bool> validate, string errorMessage)
    {
      if (message == null) throw Error.ArgumentNull("message");
      if (validate == null) throw Error.ArgumentNull("validate");
      if (errorMessage == null) throw Error.ArgumentNull("errorMessage");
      return GetStringInternal(editor, message, validate, errorMessage);
    }

    /// <summary>
    /// Gets user input for a string.
    /// </summary>
    /// <remarks>After the user enters an input, the entered value is evaluated by the validation function. If the function evaluates to true, the Prompt result is returned.
    /// If the function evaluated to false, the user is again asked for the input.</remarks>
    /// <param name="editor">The editor instance.</param>
    /// <param name="message">Input message to be displayed to the user during the prompt.</param>
    /// <param name="validate">A function that validates the user input. If it evaluates to true, the PromptResult is returned. Else, the input message is repeatedly displayed.</param>
    /// <param name="errorMessage">An error message that is displayed, if the validation function evaluates to false.</param>
    /// <returns>Returns the PromptResult.</returns>
    private static PromptResult GetStringInternal(Editor editor, string message, Func<string, bool> validate, string errorMessage)
    {
      PromptResult result = null;

      do
      {
        result = editor.GetString(message);

        if (validate(result.StringResult))
        {
          break;
        }
        else if (errorMessage != null)
        {
          editor.WriteLine(errorMessage);
        }
      }
      while (result.Status == PromptStatus.OK);

      return result;
    }
  }
}
