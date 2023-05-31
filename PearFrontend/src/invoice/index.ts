import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";

// Class to store functions to generate invoice
export default class Invoice {
  invoiceDate: Date;
  dueDate: Date;
  lineItemArray: any[];
  address: string;
  invoiceNr: number;
  toName: string;
  toEmail: string;
  toTel: string;

  static pearLogo = new Image();

  constructor(
    orderId: number,
    name: string,
    email: string,
    date: string,
    address: string,
    tel: string,
    lineItemArray: any[]
  ) {
    this.invoiceDate = new Date(date);
    this.dueDate = new Date(date);
    this.dueDate.setDate(this.invoiceDate.getDate() + 30);
    this.lineItemArray = lineItemArray;
    this.address = address;
    this.invoiceNr = orderId;
    this.toName = name;
    this.toEmail = email;
    this.toTel = tel;
    Invoice.pearLogo.src = "/img/pear-logo-white.png";
  }

  create() {
    const invoice = new jsPDF();
    const tableBody = [];
    let totalPrice = 0;

    const companyInfoY = 60;
    const companyInfoX = 15;
    const billToY = 106;
    const billToX = 15;

    for (const lineItem of this.lineItemArray) {
      tableBody.push([
        lineItem.props.id,
        lineItem.props.fullProduct,
        lineItem.props.price,
      ]);
      totalPrice += lineItem.props.price;
    }

    invoice.addImage(
      Invoice.pearLogo,
      "png",
      2,
      2,
      110,
      50,
      "pear-logo",
      "FAST"
    );
    invoice.setFontSize(12);
    invoice.text(`Invoice # ${this.invoiceNr}`, 160, 13);
    invoice.text(
      `Invoice date: ${this.dateFormater(this.invoiceDate)}`,
      160,
      20
    );
    invoice.text(`Due date: ${this.dateFormater(this.dueDate)}`, 160, 27);

    invoice.text("Pear inc.", companyInfoX, companyInfoY);
    invoice.text("Email: pear@support.com", companyInfoX, companyInfoY + 7);
    invoice.text("Address: 12 Pear rd.", companyInfoX, companyInfoY + 14);
    invoice.text("City: Pear City ", companyInfoX, companyInfoY + 21);
    invoice.text(`Tel: 000-000-0000`, companyInfoX, companyInfoY + 28);

    invoice.setFontSize(18);
    invoice.text("Bill to:", billToX, billToY);
    invoice.line(billToX - 1, billToY + 2, billToX + 62, billToY + 2);
    invoice.setFontSize(12);
    invoice.text(`Name: ${this.toName}`, billToX, billToY + 10);
    invoice.text(`Email: ${this.toEmail}`, billToX, billToY + 20);
    invoice.text(`Address: ${this.address}`, billToX, billToY + 30);
    invoice.text(`Tel: ${this.toTel}`, billToX, billToY + 40);

    invoice.setFontSize(18);
    autoTable(invoice, {
      startY: 155,
      theme: "grid",
      styles: { fontSize: 12 },
      headStyles: { fillColor: 20 },
      head: [["Id", "Product", "Price (SEK)"]],
      body: tableBody,
      footStyles: { fillColor: 20 },
      foot: [
        ["", "", `Product Prices: ${totalPrice} SEK`],
        ["", "", `Sales tax (25%): ${totalPrice * 0.25} SEK`],
        ["", "", `Shipping: ${500} SEK`],
        ["", "", `Total Price: ${totalPrice * 1.25 + 500} SEK`],
      ],
    });
    invoice.save("invoice.pdf");
  }

  private dateFormater(date: Date) {
    return date.toISOString().split("T")[0];
  }
}
