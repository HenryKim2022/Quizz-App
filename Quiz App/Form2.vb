Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Form1.TextBox1.Text
        Label4.Text = Form1.ComboBox1.Text

        Label6.Text = Val((Me.Label6.Text) * 2)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim answer As Integer
        answer = MsgBox("Are you sure you want to try again?", vbQuestion + vbYesNo + vbDefaultButton2, "Retry Again?")

        If (answer = vbYes) Then
            Label6.Text = "0"
            ClearRB()
            Form1.selectedAns = ""

            If (ClearArray(Form1.selectedAnsArrayStorage)) Then
                Me.Hide()
                Form1.Show()
                Form1.TextBox1.Text = Me.Label2.Text
                Form1.ComboBox1.Text = Me.Label4.Text

                Form1.TableLayoutPanel1.Visible = False
                Form1.TableLayoutPanel3.Visible = True
            End If
        Else
            End
        End If

    End Sub

    ''' Clearing Form1.selectedAnsArrayStorage
    Public Function ClearArray(ByRef StrArray As String()) As Boolean
        For iK As Integer = 0 To (Form1.selectedAnsArrayStorage.Count - 1)
            StrArray(iK) = ""
        Next
        Return True
    End Function

    ''' Clearing RadioButton
    Public Function ClearRB()
        For RBIndex As Integer = 0 To (2)
            Form1.RBMatrix1D(RBIndex).Checked = False
        Next
    End Function
End Class