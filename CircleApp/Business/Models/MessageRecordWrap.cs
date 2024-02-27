using Microsoft.UI;

namespace CircleApp.Business.Models;

public partial record MessageRecordWrap(MessageRecordDto Record, ContactDto Contact)
{
    public HorizontalAlignment HorizontalAlignment => this.Record.Sender.Id == this.Contact.SourceId ? HorizontalAlignment.Right : HorizontalAlignment.Left;

    public Brush BgColor => this.Record.Sender.Id == this.Contact.SourceId ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

}
