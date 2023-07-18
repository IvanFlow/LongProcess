import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-encoder',
  templateUrl: './encoder.component.html',
  changeDetection: ChangeDetectionStrategy.Default
})
export class EncoderComponent {
  public streamedResults: string[] = [];
  myForm: FormGroup = this.formBuilder.group({
    textToEncode: ['', [Validators.required]],
    encodedText: ['']
  });
  breakStreamFlag = { value: false};
  convertDisable: boolean = false;

  constructor( private readonly cd: ChangeDetectorRef, private formBuilder: FormBuilder) {
    
  }

  public encodeText() {
    if (this.myForm.invalid) {
      this.myForm.markAllAsTouched();
      return;
    }
    this.convertDisable = true;
    this.streamedResults = [];

    const textToEncode = this.myForm.controls['textToEncode']?.value;
    this.getEncodedStreamed(textToEncode);
    

  }

  public async getEncodedStreamed(textToEncode: string): Promise<void> {
    const response = await fetch(`https://localhost:7141/Encoder/EncodeBase64V1?textToEncode=${textToEncode}`, {
      allowHTTP1ForStreamingUpload: true,
    } as any);
    if (response.body == null) {
      throw new Error('Not possible in current scenario');
    }
    const reader = response.body.getReader();

    const streamedResults = this.streamedResults;
    const cd = this.cd;
    const breakFlag = this.breakStreamFlag;

    async function printStream(): Promise<void> {
      const { done, value } = await reader.read();
      if (done || breakFlag.value) {
        breakFlag.value = false;
        return;
      }
      const textDecoder = new TextDecoder();
      const stringValue = textDecoder.decode(value);

      streamedResults.push(stringValue);
      cd.markForCheck();
     
      await printStream();
    }
    
    await printStream();

    this.convertDisable = false;
  }

  validField(field: string) {
    return this.myForm.controls[field]?.errors && this.myForm.controls[field]?.touched
  }

  stopStream() {
    this.breakStreamFlag.value = true;
  }

  disableConvert(): boolean {
    return this.convertDisable;
  }
}
