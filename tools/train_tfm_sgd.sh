#!/bin/bash

# unfreeze all model and train a little more

LIBPATH=lib/yolov5-master
FROM=tfm_v4 # pretrained head tfm model
MODELPATH=$LIBPATH/runs/train/$FROM/weights/best.pt
NAME=tfm_v4_unfrozen # dia_mes_datasettrained
EPOCHS=10
PATIENCE=10
DATASET=tfm_v4

# TODO maybe should use Adam and lr=1e-5 segun keras
# https://keras.io/guides/transfer_learning

python $LIBPATH/train.py \
    --data datasets/$DATASET/data.yaml \
    --name $NAME \
    --weights $MODELPATH \
    --optimizer Adam \
    --patience $PATIENCE \
    --epochs $EPOCHS \
    --hyp $LIBPATH/data/hyps/hyp.VOC2.yaml 

# RESUME TRAINING 
# python $LIBPATH/train.py --data datasets/tfm/data.yaml --name 17_06_tfm_resume  --weights $LIBPATH/runs/train/17_06_tfm/weights/last.pt --freeze 10