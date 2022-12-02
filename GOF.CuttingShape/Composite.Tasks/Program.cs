using Composite.Tasks.Task._1;
using Composite.Tasks.Task._2;

var inputText = new InputText("myInput", "myInputValue");
var labelText = new LabelText("myLabel");
var form = new Form("myForm");
form.AddComponent(inputText);
form.AddComponent(labelText);

var inputTextString = inputText.ConvertToString();
var labelTextString = labelText.ConvertToString();

var formString = form.ConvertToString();

Console.WriteLine(formString);
Console.ReadLine();