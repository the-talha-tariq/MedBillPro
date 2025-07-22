# 🏥 Medical Billing and Financial Reporting System

A web-based enterprise application designed for hospitals, clinics, and private practices to manage medical billing, insurance claims, payments, and financial reporting efficiently. Built using **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**, this system streamlines the revenue cycle, reduces manual errors, and provides administrators with actionable financial insights.

---

## 📌 Key Features

### 1. Patient Billing
- Register patient visits and procedures
- Auto-generate itemized invoices (consultation, lab, medicines, services)
- Apply taxes, discounts, and insurance coverage
- Track invoice payment status: `Paid`, `Partially Paid`, `Unpaid`

### 2. Insurance Claim Management
- Associate bills with insurance plans
- Submit and track insurance claims
- Monitor statuses: `Submitted`, `Under Review`, `Approved`, `Rejected`
- Resubmit rejected claims

### 3. Payment Processing
- Record payments (Cash, Credit, Insurance)
- Handle overpayments and refunds
- Generate payment receipts in PDF

### 4. Financial Reports & Dashboards
- Revenue by doctor, department, or period
- Outstanding balances and claim aging reports
- Monthly/Quarterly financial summaries
- Export reports to PDF, Excel, or CSV

### 5. Role-Based Access Control (RBAC)
- **Admin**: Full system control and configuration
- **Accountant**: Access to billing and financial sections
- **Doctor/Staff**: Generate bills for visits
- **Patient (optional)**: View own invoices and claim history

---

## 🛠️ Tech Stack

| Layer             | Technology                        |
|------------------|-----------------------------------|
| Backend API       | ASP.NET Core Web API              |
| Frontend          | Razor Pages / Blazor Server       |
| Database          | SQL Server + Entity Framework Core|
| Authentication    | ASP.NET Identity + JWT            |
| Reporting Export  | EPPlus / Syncfusion               |
| Deployment        | Azure App Service / IIS           |

---

## 🗃️ Example Database Tables

- `Patients` (Id, Name, DOB, InsuranceId, Contact)
- `Doctors` (Id, Name, Department, Fee)
- `Visits` (Id, PatientId, DoctorId, Date, Notes)
- `Invoices` (Id, VisitId, TotalAmount, Discount, Tax, Status)
- `InvoiceItems` (Id, InvoiceId, ServiceName, Quantity, Price)
- `Payments` (Id, InvoiceId, Amount, PaymentMethod, Date)
- `InsuranceClaims` (Id, InvoiceId, InsuranceId, Status, Dates)

---

## 📈 Sample Reports

| Report | Description |
|--------|-------------|
| Revenue Report | Income grouped by doctor or department |
| Outstanding Balances | Patients with unpaid bills |
| Claim Aging | Track insurance claim turnaround time |
| Monthly Summary | Revenue and expense overview |

---

## 📦 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/the-talha-tariq/MedBillPro.git
cd MedBillPro
