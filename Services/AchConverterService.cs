using System.Text;

namespace JsonNachaAchApi.Services
{
    public class AchConverterService : IAchConverterService
    {
        public async Task<string> ConvertToAchAsync(AchFile achFile)
        {
            var sb = new StringBuilder();

            // File Header Record
            sb.AppendLine(FormatFileHeader(achFile.FileHeader));

            // Process IAT Batches
            if (achFile.IATBatches != null)
            {
                foreach (var batch in achFile.IATBatches)
                {
                    sb.AppendLine(FormatIATBatchHeader(batch.IATBatchHeader));

                    foreach (var entry in batch.IATEntryDetails)
                    {
                        sb.AppendLine(FormatIATEntryDetail(entry));
                        sb.AppendLine(FormatAddenda10(entry.Addenda10));
                        sb.AppendLine(FormatAddenda11(entry.Addenda11));
                        sb.AppendLine(FormatAddenda12(entry.Addenda12));
                        sb.AppendLine(FormatAddenda13(entry.Addenda13));
                        sb.AppendLine(FormatAddenda14(entry.Addenda14));
                        sb.AppendLine(FormatAddenda15(entry.Addenda15));
                        sb.AppendLine(FormatAddenda16(entry.Addenda16));
                    }

                    sb.AppendLine(FormatBatchControl(batch.BatchControl));
                }
            }

            // Process Regular Batches
            if (achFile.Batches != null)
            {
                foreach (var batch in achFile.Batches)
                {
                    sb.AppendLine(FormatBatchHeader(batch.BatchHeader));

                    foreach (var entry in batch.EntryDetails)
                    {
                        sb.AppendLine(FormatEntryDetail(entry));
                    }

                    sb.AppendLine(FormatBatchControl(batch.BatchControl));
                }
            }

            // File Control Record
            sb.AppendLine(FormatFileControl(achFile.FileControl));

            return await Task.FromResult(sb.ToString());
        }

        private string FormatFileHeader(FileHeader header)
        {
            // Format: Record Type(1), Priority Code(2), Immediate Destination(10), Immediate Origin(10),
            // File Creation Date(6), File Creation Time(4), File ID Modifier(1), Record Size(3),
            // Blocking Factor(2), Format Code(1), Immediate Destination Name(23), Immediate Origin Name(23), Reference Code(8)
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                "1",  // Record Type
                "01", // Priority Code
                header.ImmediateDestination.PadLeft(10, ' '),
                header.ImmediateOrigin.PadLeft(10, ' '),
                header.FileCreationDate.PadRight(6, ' '),
                header.FileCreationTime.PadRight(4, ' '),
                header.FileIDModifier.PadRight(1, 'A'),
                "094", // Record Size
                "10",  // Blocking Factor
                "1",   // Format Code
                header.ImmediateDestinationName.PadRight(23, ' '),
                header.ImmediateOriginName.PadRight(23, ' '),
                "".PadRight(8, ' ')  // Reference Code
            ).PadRight(94, ' ');
        }

        private string FormatIATBatchHeader(IATBatchHeader header)
        {
            // Format: Record Type(1), Service Class Code(3), Entry Description(16), Company Name(16),
            // Company Discretionary Data(20), Company ID(10), SEC Code(3), Entry/Addenda(1),
            // Effective Entry Date(6), Settlement Date(3), Originator Status(1), Originating DFI ID(8), Batch Number(7)
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                "5",
                header.ServiceClassCode.ToString().PadLeft(3, '0'),
                header.IATIndicator.PadRight(16, ' '),
                header.CompanyEntryDescription.PadRight(16, ' '),
                "".PadRight(20, ' '), // Company Discretionary Data
                header.OriginatorIdentification.PadRight(10, ' '),
                header.StandardEntryClassCode.PadRight(3, ' '),
                "1", // Entry/Addenda Flag
                header.EffectiveEntryDate.PadRight(6, ' '),
                header.SettlementDate.PadRight(3, ' '),
                header.OriginatorStatusCode.ToString(),
                header.ODFIIdentification.PadRight(8, ' '),
                header.BatchNumber.ToString().PadLeft(7, '0')
            ).PadRight(94, ' ');
        }

        private string FormatIATEntryDetail(IATEntryDetail entry)
        {
            // Format: Record Type(1), Transaction Code(2), RDFI ID(8), Check Digit(1), DFI Account(17),
            // Amount(10), ID Number(15), Individual Name(22), Discretionary Data(2), Addenda(1),
            // Trace Number(15)
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                "6",
                entry.TransactionCode.ToString().PadLeft(2, '0'),
                entry.RDFIIdentification.PadRight(8, ' '),
                entry.CheckDigit,
                entry.DFIAccountNumber.PadRight(17, ' '),
                entry.Amount.ToString().PadLeft(10, '0'),
                entry.OFACScreeningIndicator.PadRight(15, ' '),
                "".PadRight(22, ' '), // Individual Name
                "  ", // Discretionary Data
                entry.AddendaRecordIndicator.ToString(),
                entry.TraceNumber.PadRight(15, ' ')
            ).PadRight(94, ' ');
        }

        private string FormatAddenda10(Addenda10 addenda)
        {
            // Format: Record Type(1), Addenda Type(2), Transaction Type(3), Foreign Payment Amount(18),
            // Name(35), Entry Detail Sequence Number(7), Addenda Sequence Number(4), Terminal ID(6),
            // Terminal Location(27), Terminal City(2), Terminal State(2), Trace Number(15)
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                "7",
                addenda.TypeCode.PadRight(2, ' '),
                addenda.TransactionTypeCode.PadRight(3, ' '),
                addenda.ForeignPaymentAmount.ToString().PadLeft(18, '0'),
                addenda.Name.PadRight(35, ' '),
                addenda.EntryDetailSequenceNumber.ToString().PadLeft(7, '0'),
                "0001", // Addenda Sequence Number
                "".PadRight(24, ' ') // Reserved
            ).PadRight(94, ' ');
        }

        private string FormatAddenda11(Addenda11 addenda)
        {
            return string.Format("{0,-1}{1,-2}{2,-35}{3,-35}",
                "7", // Record Type Code
                addenda.TypeCode,
                addenda.OriginatorName.PadRight(35, ' '),
                addenda.OriginatorStreetAddress.PadRight(35, ' ')
            );
        }

        private string FormatAddenda12(Addenda12 addenda)
        {
            return string.Format("{0,-1}{1,-2}{2,-35}{3,-35}",
                "7", // Record Type Code
                addenda.TypeCode,
                addenda.OriginatorCityStateProvince.PadRight(35, ' '),
                addenda.OriginatorCountryPostalCode.PadRight(35, ' ')
            );
        }

        private string FormatAddenda13(Addenda13 addenda)
        {
            return string.Format("{0,-1}{1,-2}{2,-2}{3,-3}{4,-35}{5,-2}",
                "7", // Record Type Code
                addenda.TypeCode,
                addenda.ODFIIDNumberQualifier,
                addenda.ODFIIdentification,
                addenda.ODFIName.PadRight(35, ' '),
                addenda.ODFIBranchCountryCode
            );
        }

        private string FormatAddenda14(Addenda14 addenda)
        {
            return string.Format("{0,-1}{1,-2}{2,-2}{3,-3}{4,-35}{5,-2}",
                "7", // Record Type Code
                addenda.TypeCode,
                addenda.RDFIIDNumberQualifier,
                addenda.RDFIIdentification,
                addenda.RDFIName.PadRight(35, ' '),
                addenda.RDFIBranchCountryCode
            );
        }

        private string FormatAddenda15(Addenda15 addenda)
        {
            return string.Format("{0,-1}{1,-2}{2,-15}{3,-35}",
                "7", // Record Type Code
                addenda.TypeCode,
                addenda.ReceiverIDNumber.PadRight(15, ' '),
                addenda.ReceiverStreetAddress.PadRight(35, ' ')
            );
        }

        private string FormatAddenda16(Addenda16 addenda)
        {
            return string.Format("{0,-1}{1,-2}{2,-35}{3,-35}",
                "7", // Record Type Code
                addenda.TypeCode,
                addenda.ReceiverCityStateProvince.PadRight(35, ' '),
                addenda.ReceiverCountryPostalCode.PadRight(35, ' ')
            );
        }

        private string FormatBatchHeader(BatchHeader header)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
        "5",
        header.ServiceClassCode.ToString().PadLeft(3, '0'),
        header.CompanyName.PadRight(16, ' '),
        "".PadRight(20, ' '), // Company Discretionary Data
        header.CompanyIdentification.PadRight(10, ' '),
        header.StandardEntryClassCode.PadRight(3, ' '),
        header.CompanyEntryDescription.PadRight(10, ' '),
        DateTime.Now.ToString("yyMMdd"), // Company Descriptive Date
        header.EffectiveEntryDate.PadRight(6, ' '),
        header.SettlementDate.PadRight(3, ' '),
        header.OriginatorStatusCode.ToString(),
        header.ODFIIdentification.PadRight(8, ' '),
        header.BatchNumber.ToString().PadLeft(7, '0')
    ).PadRight(94, ' ');
        }

        private string FormatEntryDetail(EntryDetail entry)
        {
            return string.Format("{0,-1}{1,-2}{2,-8}{3,-1}{4,-17}{5,-10}{6,-15}{7,-22}{8,-2}",
                "6", // Record Type Code
                entry.TransactionCode.ToString().PadLeft(2, '0'),
                entry.RDFIIdentification,
                entry.CheckDigit,
                entry.DFIAccountNumber.PadRight(17, ' '),
                entry.Amount.ToString().PadLeft(10, '0'),
                entry.IndividualName.PadRight(15, ' '),
                entry.TraceNumber.PadRight(22, ' '),
                entry.DiscretionaryData.PadRight(2, ' ')
            );
        }

        private string FormatBatchControl(BatchControl control)
        {
            return string.Format("{0,-1}{1,-3}{2,-6}{3,-10}{4,-12}{5,-10}{6,-12}",
                "8", // Record Type Code
                control.ServiceClassCode.ToString().PadLeft(3, '0'),
                control.EntryAddendaCount.ToString().PadLeft(6, '0'),
                control.EntryHash.ToString().PadLeft(10, '0'),
                control.TotalDebit.ToString().PadLeft(12, '0'),
                control.TotalCredit.ToString().PadLeft(12, '0'),
                control.CompanyIdentification.PadRight(10, ' '),
                "".PadRight(19, ' '), // Message Authentication
                "".PadRight(6, ' '),  // Reserved
                control.ODFIIdentification.PadRight(8, ' '),
                control.BatchNumber.ToString().PadLeft(7, '0')
            ).PadRight(94, ' ');
        }

        private string FormatFileControl(FileControl control)
        {
            return string.Format("{0,-1}{1,-6}{2,-6}{3,-8}{4,-10}{5,-12}{6,-12}",
                "9", // Record Type Code
                control.BatchCount.ToString().PadLeft(6, '0'),
                control.BlockCount.ToString().PadLeft(6, '0'),
                control.EntryAddendaCount.ToString().PadLeft(8, '0'),
                control.EntryHash.ToString().PadLeft(10, '0'),
                control.TotalDebit.ToString().PadLeft(12, '0'),
                control.TotalCredit.ToString().PadLeft(12, '0')
            );
        }
    }
} 