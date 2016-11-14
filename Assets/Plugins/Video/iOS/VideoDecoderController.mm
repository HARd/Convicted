#import <UIKit/UIKit.h>
#import "UnityAppController.h"

extern "C" void VideoDecoderUnitySetGraphicsDevice(void* device, int deviceType, int eventType);
extern "C" void VideoDecoderUnityRenderEvent(int marker);

@interface VideoDecoderController : UnityAppController
{
}
- (void)shouldAttachRenderDelegate;
@end

@implementation VideoDecoderController

- (void)shouldAttachRenderDelegate;
{
	UnityRegisterRenderingPlugin(&VideoDecoderUnitySetGraphicsDevice, &VideoDecoderUnityRenderEvent);
}
@end


IMPL_APP_CONTROLLER_SUBCLASS(VideoDecoderController)

