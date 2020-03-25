using JetBrains.Annotations;

namespace Lykke.Service.Vouchers.Client.Models.Vouchers
{
    /// <summary>
    /// Specifies voucher status.
    /// </summary>
    [PublicAPI]
    public enum VoucherStatus
    {
        /// <summary>
        /// Unspecified status.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the voucher is stock. 
        /// </summary>
        InStock,

        /// <summary>
        /// Indicated that the voucher reserved by customer and waiting for payment.
        /// </summary>
        Reserved,

        /// <summary>
        /// Indicates that the voucher bought by a customer. 
        /// </summary>
        Sold
    }
}
