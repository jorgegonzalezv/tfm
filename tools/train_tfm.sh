#!/bin/bash

# train yolo pretrained coco over unity dataset 

LIBPATH=lib/yolov5-master
NAME=tfm_v4 # dia_mes_datasettrained
EPOCHS=20  # best: 17 , less best: 10? 
PATIENCE=10
DATASET=tfm_v4

# patience: for early stopping
# from pretrained coco yolov5s.pt
# freeze backbone of pretrained model: freeze 10
python $LIBPATH/train.py \
    --data datasets/$DATASET/data.yaml \
    --name $NAME \
    --weights $LIBPATH/yolov5s.pt \
    --optimizer Adam \
    --freeze 10 \
    --patience $PATIENCE \
    --epochs $EPOCHS \
    --hyp $LIBPATH/data/hyps/hyp.VOC.yaml  # https://github.com/ultralytics/yolov5/issues/6820

# RESUME TRAINING 
# python $LIBPATH/train.py --data datasets/tfm/data.yaml --name 17_06_tfm_resume  --weights $LIBPATH/runs/train/17_06_tfm/weights/last.pt --freeze 10