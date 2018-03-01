var kh = {
    init: function () {
        kh.DSTenTour();
        
    },
    DSTenTour:function()
    {
        
    },
    DanhsachDoan: function(){
        var html = '';
        $.ajax({
            url:'/KhachHang/DanhsachDoan',
            type: "POST",
            dataType: "json",
            success: function (res) {
                if(res.status==true)
                {
                    html = '<option value="">--Chọn đoàn--</option>';
                    var data = res.data;
                    $.each(data,function(i,item){
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>';
                    });
                    
                    $("#ddlDoan").html(html);
                }
            }
        })
    }
}
kh.init();