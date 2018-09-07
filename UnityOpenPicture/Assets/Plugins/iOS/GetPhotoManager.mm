//
//  GetPhotoControl.m
//  image2
//
//  Created by tengjiang on 16/4/26.
//  Copyright © 2016年 GAEA. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "GetPhotoManager.h"
#include <sys/param.h>
#include <sys/mount.h>
#import <AssetsLibrary/AssetsLibrary.h>
#import <AVFoundation/AVCaptureDevice.h>
#import <AVFoundation/AVMediaFormat.h>

extern "C"
{
    typedef void(*PhotoCallBack)(void* p);
    
    void _GetPhotoControl(int index);
    void _GetPhotoControlPlus(int index, float editWidth, float editHeight, float editCompress, bool isEdit);

    void _SetPhotoSuccessCallback(PhotoCallBack cb);
    void _SetPhotoCancelCallback(PhotoCallBack cb);
}

//static GetPhotoManager *getPhotoControl;
static PhotoCallBack gSuccessCB;
static PhotoCallBack gCancelCB;

void _GetPhotoControl(int index)
{
    [[GetPhotoManager shareInstance] GetPhotoChoose:index];
}

void _GetPhotoControlPlus(int index, float editWidth, float editHeight, float editCompress, bool isEdit)
{
    [[GetPhotoManager shareInstance] GetPhotoChoosePlus:index
                                              editWidth:editWidth
                                             editHeight:editHeight
                                           editCompress:editCompress
                                                 isEdit:isEdit];
}

void _SetPhotoSuccessCallback(PhotoCallBack cb)
{
    gSuccessCB = cb;
}

void _SetPhotoCancelCallback(PhotoCallBack cb)
{
    gCancelCB = cb;
}

@implementation GetPhotoManager
typedef enum {
    kCLAuthorizationStatusNotDetermined = 0, // 用户尚未做出选择这个应用程序的问候
    kCLAuthorizationStatusRestricted,        // 此应用程序没有被授权访问的照片数据。可能是家长控制权限
    kCLAuthorizationStatusDenied,            // 用户已经明确否认了这一照片数据的应用程序访问
    kCLAuthorizationStatusAuthorized         // 用户已经授权应用访问照片数据} CLAuthorizationStatus;
}kCLAuthorizationStatus;

//单例模式
static GetPhotoManager* _instance = nil;

+(instancetype) shareInstance
{
    if (_instance == nil)
    {
        static dispatch_once_t onceToken;

        dispatch_once(&onceToken, ^{
            _instance = [[super allocWithZone:NULL] init] ;
        }) ;
    }
    
    return _instance;
}

+(id) allocWithZone:(struct _NSZone *)zone
{
    return [GetPhotoManager shareInstance] ;
}

-(id) copyWithZone:(struct _NSZone *)zone
{
    return [GetPhotoManager shareInstance] ;
}

NSInteger m_nChooseType = 0;
CGFloat m_fEditWidth = 0;
CGFloat m_fEditHeight = 0;
CGFloat m_fEditCompress = 0;


- (void)SetChooseType:(NSInteger)chooseType
{
    m_nChooseType = chooseType;
}

- (void)SetEditData:(CGFloat)editWidth editHeight:(CGFloat)editHeight editCompress:(CGFloat)editCompress
{
    m_fEditWidth = editWidth;
    m_fEditHeight = editHeight;
    m_fEditCompress = editCompress;
}

- (NSInteger)GetChooseType
{
    return m_nChooseType;
}

- (CGFloat)GetEditWidth
{
    return m_fEditWidth;
}

- (CGFloat)GetEditHeight
{
    return m_fEditHeight;
}

- (CGFloat)GetEditCompress
{
    return m_fEditCompress;
}

- (void)GetPhotoChoose:(NSInteger)ChooseIndex{
    //支持老方法 设置为0
    [self SetChooseType:0];
    
    UIImagePickerControllerSourceType sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    
    NSInteger resultIndex = [self CheckChooseIndex:ChooseIndex];
    if(resultIndex == -1)
    {
        return;
    }
    sourceType = [self GetSourchTypeByIndex:resultIndex];
    
    // 跳转到相机或相册页面
    UIImagePickerController *imagePickerController = [[UIImagePickerController alloc] init];
    imagePickerController.delegate = self;
    //imagePickerController.allowsEditing = NO;
    imagePickerController.sourceType = sourceType;
    
    [UnityGetGLViewController() presentViewController:imagePickerController animated:YES completion:^{
        
    }];
}

- (void)GetPhotoChoosePlus:(NSInteger)ChooseIndex editWidth:(CGFloat)editWidth editHeight:(CGFloat)editHeight editCompress:(CGFloat)editCompress isEdit:(BOOL)isEdit
{
    //新方法 设置为1
    [self SetChooseType:1];
    //设置裁剪宽、高、缩放
    [self SetEditData:editWidth editHeight:editHeight editCompress:editCompress];
    
    UIImagePickerControllerSourceType sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    
    NSInteger resultIndex = [self CheckChooseIndex:ChooseIndex];
    if(resultIndex == -1)
    {
        return;
    }
    sourceType = [self GetSourchTypeByIndex:resultIndex];
    
    // 跳转到相机或相册页面
    UIImagePickerController *imagePickerController = [[UIImagePickerController alloc] init];
    imagePickerController.delegate = self;
    imagePickerController.allowsEditing = isEdit;
    imagePickerController.sourceType = sourceType;
    
	imagePickerController.modalPresentationStyle = UIModalPresentationFormSheet;
	
    [UnityGetGLViewController() presentViewController:imagePickerController animated:YES completion:^{
        
    }];
}



- (NSInteger)CheckChooseIndex:(NSInteger)ChooseIndex
{
    if([UIImagePickerController isSourceTypeAvailable:UIImagePickerControllerSourceTypeCamera]) {
        switch (ChooseIndex) {
            case 0:
                //来源:相机
                if([self GetCameraPermission] == -1)
                {
                    UnitySendMessage("MainCameraFunction", "CandleShowPhoto", "");
                    char msg[256] = {0};
                    strcpy(msg, "CancelShowPhoto - 相机");
                    gCancelCB(msg);
                    return -1;
                }
                return ChooseIndex;
                break;
            case 1:
                //来源:相册
                if([self GetPickPermission] == -1)
                {
                    UnitySendMessage("MainCameraFunction", "CandleShowPhoto", "");
                    char msg[256] = {0};
                    strcpy(msg, "CancelShowPhoto - 相册");
                    gCancelCB(msg);
                    return -1;
                }
                return ChooseIndex;
                break;
        }
    }
    else
    {
        if([self GetPickPermission] == -1)
        {
            UnitySendMessage("MainCameraFunction", "CandleShowPhoto", "");
            char msg[256] = {0};
            strcpy(msg, "CancelShowPhoto - Permission Failed");
            gCancelCB(msg);
            return -1;
        }
        return ChooseIndex;
    }
    return -1;
}

- (UIImagePickerControllerSourceType)GetSourchTypeByIndex:(NSInteger)ChooseIndex
{
    UIImagePickerControllerSourceType sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    switch (ChooseIndex) {
        case 0:
            //相机
            sourceType = UIImagePickerControllerSourceTypeCamera;
            break;
          
        case 1:
            //相册
            sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
            break;
    }
    return sourceType;
}

- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary<NSString *,id> *)info
{
    if([self GetChooseType] == 0)
    {
        [picker dismissViewControllerAnimated:YES completion:^{
            
        }];
        UIImage *image = [info objectForKey:UIImagePickerControllerOriginalImage];
        NSData *fData = [self imageCompressForWidth:image targetWidth:600 targetHeight:400];
        NSString *stringPhoto = [fData base64EncodedStringWithOptions:0];
        UnitySendMessage("MainCameraFunction", "ShowPhoto", [stringPhoto UTF8String]);
        
        NSData *fDataSmall = [self imageCompressForWidth:image targetWidth:80 targetHeight:60];
        NSString *stringPhotoSmall = [fDataSmall base64EncodedStringWithOptions:0];
        UnitySendMessage("MainCameraFunction", "SaveMiniPhoto", [stringPhotoSmall UTF8String]);
    }
    else
    {
        [picker dismissViewControllerAnimated:YES completion:^{
            
        }];
        CGFloat editWidth = [self GetEditWidth];
        CGFloat editHeight = [self GetEditHeight];
//        CGFloat editCompress = [self GetEditCompress]*0.01;
        UIImage *image = [info objectForKey:UIImagePickerControllerEditedImage];
        if(image == nil)
        {
            image = [info objectForKey:UIImagePickerControllerOriginalImage];
        }
        NSData *fData = [self imageCompressForWidth:image targetWidth:editWidth targetHeight:editHeight];
        NSString *stringPhoto = [fData base64EncodedStringWithOptions:0];
        
//        UnitySendMessage("MainCameraFunction", "ShowPhotoPlus", [stringPhoto UTF8String]);
        const char* msg = [stringPhoto UTF8String];
        gSuccessCB((void*)msg);
        
        fData = nil;
    }
}

- (void)imagePickerControllerDidCancel:(UIImagePickerController *)picker
{
    NSLog(@"您取消了选择图片");
    [picker dismissViewControllerAnimated:YES completion:^{
        
    }];
	UnitySendMessage("MainCameraFunction", "CandleShowPhoto", "");
    
    char msg[256] = {0};
    strcpy(msg, "CancelShowPhoto - 取消了选择图片");
    gCancelCB(msg);
}

- (NSData *) imageCompressForWidth:(UIImage *)sourceImage targetWidth:(CGFloat)defineWidth targetHeight:(CGFloat)defineHeight
{
    CGSize imageSize = sourceImage.size;
    CGFloat width = imageSize.width;
    CGFloat height = imageSize.height;
    CGFloat targetWidth = defineWidth;
    CGFloat targetHeight = (targetWidth / width) * height;
    if(targetHeight > defineHeight)
    {
        targetHeight = defineHeight;
        targetWidth = (targetHeight / height) * width;
    }
    UIGraphicsBeginImageContext(CGSizeMake(targetWidth, targetHeight));
    [sourceImage drawInRect:CGRectMake(0,0,targetWidth,  targetHeight)];
    UIImage* newImage = UIGraphicsGetImageFromCurrentImageContext();
    UIGraphicsEndImageContext();
//    NSData *fData = UIImagePNGRepresentation(newImage);
    NSData *fData = UIImageJPEGRepresentation(newImage, 1);
    return fData;
}

-(int) GetCameraPermission
{
    NSString *mediaType = AVMediaTypeVideo;
    AVAuthorizationStatus authStatus = [AVCaptureDevice authorizationStatusForMediaType:mediaType];

    if(authStatus == AVAuthorizationStatusRestricted|| authStatus == AVAuthorizationStatusDenied){
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"提示"
                                                        message:@"请在设备的设置-隐私-相机中允许访问相机。"
                                                       delegate:self
                                              cancelButtonTitle:@"确定"
                                              otherButtonTitles:nil];
        [alert show];
        return -1;
    }
    else if(authStatus == AVAuthorizationStatusAuthorized){//允许访问
        return 0;
        
    }
    return 0;
}

-(int) GetPickPermission
{
    ALAuthorizationStatus author = [ALAssetsLibrary authorizationStatus];
    if (author == kCLAuthorizationStatusRestricted || author == kCLAuthorizationStatusDenied){
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"提示"
                                                        message:@"请在设备的设置-隐私-照片中允许访问照片。"
                                                       delegate:self
                                              cancelButtonTitle:@"确定"
                                              otherButtonTitles:nil];
        [alert show];
        return -1;
    }
    return 0;
}

@end
