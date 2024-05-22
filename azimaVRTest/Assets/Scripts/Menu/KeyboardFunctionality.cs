using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardFunctionality : MonoBehaviour
{
    public TMP_InputField houseSearch;
    public GameObject letterKeyBoard;
    public GameObject symbolKeyBoard;
    public bool capital;

    public void BackButtonPressed()
    {
        houseSearch.text = houseSearch.text.Substring(0, houseSearch.text.Length - 1);
    }

    public void SymbolButtonPressed()
    {
        letterKeyBoard.SetActive(false);
        symbolKeyBoard.SetActive(true);
    }

    public void LetterButtonPressed()
    {
        letterKeyBoard.SetActive(true);
        symbolKeyBoard.SetActive(false);
    }

    public void APressed()
    {
        if (capital){
            houseSearch.text += 'A';
        }
        else{
            houseSearch.text += 'a';
        }
    }

    public void BPressed()
    {
        if (capital)
        {
            houseSearch.text += 'B';
        }
        else
        {
            houseSearch.text += 'b';
        }
    }

    public void CPressed()
    {
        if (capital)
        {
            houseSearch.text += 'C';
        }
        else
        {
            houseSearch.text += 'c';
        }
    }

    public void DPressed()
    {
        if (capital)
        {
            houseSearch.text += 'D';
        }
        else
        {
            houseSearch.text += 'd';
        }
    }

    public void EPressed()
    {
        if (capital)
        {
            houseSearch.text += 'E';
        }
        else
        {
            houseSearch.text += 'e';
        }
    }

    public void FPressed()
    {
        if (capital)
        {
            houseSearch.text += 'F';
        }
        else
        {
            houseSearch.text += 'f';
        }
    }

    public void GPressed()
    {
        if (capital)
        {
            houseSearch.text += 'G';
        }
        else
        {
            houseSearch.text += 'g';
        }
    }

    public void HPressed()
    {
        if (capital)
        {
            houseSearch.text += 'H';
        }
        else
        {
            houseSearch.text += 'h';
        }
    }

    public void IPressed()
    {
        if (capital)
        {
            houseSearch.text += 'I';
        }
        else
        {
            houseSearch.text += 'i';
        }
    }

    public void JPressed()
    {
        if (capital)
        {
            houseSearch.text += 'J';
        }
        else
        {
            houseSearch.text += 'j';
        }
    }

    public void KPressed()
    {
        if (capital)
        {
            houseSearch.text += 'K';
        }
        else
        {
            houseSearch.text += 'k';
        }
    }

    public void LPressed()
    {
        if (capital)
        {
            houseSearch.text += 'L';
        }
        else
        {
            houseSearch.text += 'l';
        }
    }

    public void MPressed()
    {
        if (capital)
        {
            houseSearch.text += 'M';
        }
        else
        {
            houseSearch.text += 'm';
        }
    }

    public void NPressed()
    {
        if (capital)
        {
            houseSearch.text += 'N';
        }
        else
        {
            houseSearch.text += 'n';
        }
    }

    public void OPressed()
    {
        if (capital)
        {
            houseSearch.text += 'O';
        }
        else
        {
            houseSearch.text += 'o';
        }
    }

    public void PPressed()
    {
        if (capital)
        {
            houseSearch.text += 'P';
        }
        else
        {
            houseSearch.text += 'p';
        }
    }

    public void QPressed()
    {
        if (capital)
        {
            houseSearch.text += 'Q';
        }
        else
        {
            houseSearch.text += 'q';
        }
    }

    public void RPressed()
    {
        if (capital)
        {
            houseSearch.text += 'R';
        }
        else
        {
            houseSearch.text += 'r';
        }
    }

    public void SPressed()
    {
        if (capital)
        {
            houseSearch.text += 'S';
        }
        else
        {
            houseSearch.text += 's';
        }
    }

    public void TPressed()
    {
        if (capital)
        {
            houseSearch.text += 'T';
        }
        else
        {
            houseSearch.text += 't';
        }
    }

    public void UPressed()
    {
        if (capital)
        {
            houseSearch.text += 'U';
        }
        else
        {
            houseSearch.text += 'u';
        }
    }

    public void VPressed()
    {
        if (capital)
        {
            houseSearch.text += 'V';
        }
        else
        {
            houseSearch.text += 'v';
        }
    }

    public void WPressed()
    {
        if (capital)
        {
            houseSearch.text += 'W';
        }
        else
        {
            houseSearch.text += 'w';
        }
    }

    public void XPressed()
    {
        if (capital)
        {
            houseSearch.text += 'X';
        }
        else
        {
            houseSearch.text += 'x';
        }
    }

    public void YPressed()
    {
        if (capital)
        {
            houseSearch.text += 'Y';
        }
        else
        {
            houseSearch.text += 'y';
        }
    }

    public void ZPressed()
    {
        if (capital)
        {
            houseSearch.text += 'Z';
        }
        else
        {
            houseSearch.text += 'z';
        }
    }

    public void CommaPressed()
    {
        houseSearch.text += ',';
    }

    public void DotPressed()
    {
        houseSearch.text += '.';
    }

    public void ForwardSlashPressed()
    {
        houseSearch.text += '/';
    }

    public void SpacePressed()
    {
        houseSearch.text += ' ';
    }

    public void BackTickPressed()
    {
        houseSearch.text += '`';
    }

    public void OnePressed()
    {
        houseSearch.text += '1';
    }

    public void TwoPressed()
    {
        houseSearch.text += '2';
    }

    public void ThreePressed()
    {
        houseSearch.text += '3';
    }

    public void FourPressed()
    {
        houseSearch.text += '4';
    }

    public void FivePressed()
    {
        houseSearch.text += '5';
    }

    public void SixPressed()
    {
        houseSearch.text += '6';
    }

    public void SevenPressed()
    {
        houseSearch.text += '7';
    }

    public void EightPressed()
    {
        houseSearch.text += '8';
    }

    public void NinePressed()
    {
        houseSearch.text += '9';
    }

    public void ZeroPressed()
    {
        houseSearch.text += '0';
    }

    public void PlusPressed()
    {
        houseSearch.text += '+';
    }

    public void HyphenPressed()
    {
        houseSearch.text += '-';
    }

    public void EqualsPressed()
    {
        houseSearch.text += '=';
    }

    public void UnderScorePressed()
    {
        houseSearch.text += '_';
    }

    public void FirstArrowPressed()
    {
        houseSearch.text += '<';
    }

    public void SecondArrowPressed()
    {
        houseSearch.text += '>';
    }

    public void OpeningSquareBracketPressed()
    {
        houseSearch.text += '[';
    }

    public void ClosingSquareBracketPressed()
    {
        houseSearch.text += ']';
    }

    public void BackSlashPressed()
    {
        houseSearch.text += '\\';
    }

    public void ExclamationPressed()
    {
        houseSearch.text += '!';
    }

    public void AtPressed()
    {
        houseSearch.text += '@';
    }

    public void HashtagPressed()
    {
        houseSearch.text += '#';
    }

    public void DollarSignPressed()
    {
        houseSearch.text += '$';
    }

    public void PercentagePressed()
    {
        houseSearch.text += '%';
    }

    public void UpArrowPressed()
    {
        houseSearch.text += '^';
    }

    public void AmpersandPressed()
    {
        houseSearch.text += '&';
    }

    public void AsteriskPressed()
    {
        houseSearch.text += '*';
    }

    public void OpenBracketPressed()
    {
        houseSearch.text += '(';
    }

    public void ClosedBracketPressed()
    {
        houseSearch.text += ')';
    }

    public void SingleQuotePressed()
    {
        houseSearch.text += '\'';
    }

    public void DoubleQuotePressed()
    {
        houseSearch.text += '\"';
    }

    public void ColonPressed()
    {
        houseSearch.text += ':';
    }

    public void SemiColonPressed()
    {
        houseSearch.text += ';';
    }

    public void TildaPressed()
    {
        houseSearch.text += '~';
    }

    public void QuestionPressed()
    {
        houseSearch.text += '?';
    }

    public void OpenSquigglyPressed()
    {
        houseSearch.text += '{';
    }

    public void ClosedSquigglePressed()
    {
        houseSearch.text += '}';
    }
}
