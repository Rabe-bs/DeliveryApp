namespace DeliveryApp.DeliveryApp.Domain.Enums
{
    public enum OrderStatus
    {
        Pending, //  انتظار تاكيد الدفع او ارساله 
        SentToRestaurant,// تم ارسال الطلب للمطعم المطلوب 
        AcceptedByRestaurant,// تاكد المطعم المطلوب من وجود  الطلب وقبولة 
        RejectedByRestaurant,// اعادة للمحفظة في حال رفض الطلب المطعم 
        OutForDelivery,// الطلب مع السواق وفي طريقة 
        Delivered,// تم تسليم الطلب للعميل 
        CancelledByCustomer,// الفاء العميل للطلب والتحقق متى وقت الالغاء 
        FailedDueToError //حدث تقني او فشل في النظام استعادة الاموال للمحفظة 
    }
}
